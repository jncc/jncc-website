using Examine;
using JNCC.PublicWebsite.Core.Configuration;
using JNCC.PublicWebsite.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Indexers
{
    public class ElasticPublishedContentMediaIndexer : UmbracoExamine.UmbracoContentIndexer
    {
        private SearchService _searchService;
        private readonly ISearchConfiguration _searchConfiguration;

        public ElasticPublishedContentMediaIndexer() : base()
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
            var config = SearchConfiguration.GetConfig();
            _searchService = new SearchService(config);
        }

        // let Umbraco know that we support Content and Media
        private static List<string> _supportedTypes = new List<string>() { UmbracoExamine.IndexTypes.Content, UmbracoExamine.IndexTypes.Media };

        private UmbracoHelper _umbracoHelper;

        protected override IEnumerable<string> SupportedTypes => _supportedTypes;

        public override void ReIndexNode(XElement node, string type)
        {
            if (!_supportedTypes.Contains(type))
                return;

            AddSingleNodeToIndex(node, type);

        }

        protected override void AddSingleNodeToIndex(XElement node, string type)
        {
            DataService.LogService.AddVerboseLog((int)node.Attribute("id"), string.Format("AddSingleNodeToIndex with type: {0}", type));

            AddNodesToQueue(new XElement[] { node }, type);
        }

        public override void DeleteFromIndex(string nodeId)
        {
            // delete the node from index
            _searchService.DeleteFromIndex(nodeId);
            //LogHelper.Info<SearchService>($"Removed document with id: {nodeId} from index");

        }

        public override void RebuildIndex()
        {
            LogHelper.Info<SearchService>($"Rebuild Elastic Index triggered");

            base.RebuildIndex();
        }

        protected override void PerformIndexAll(string type)
        {
            if (!SupportedTypes.Contains(type))
                return;

            var xPath = "//*[(number(@id) > 0 and (@isDoc or @nodeTypeAlias)){0}]"; //we'll add more filters to this below if needed

            var sb = new StringBuilder();

            //create the xpath statement to match node type aliases if specified
            if (IndexerData.IncludeNodeTypes.Any())
            {
                sb.Append("(");
                foreach (var field in IndexerData.IncludeNodeTypes)
                {
                    //this can be used across both schemas
                    const string nodeTypeAlias = "(@nodeTypeAlias='{0}' or (count(@nodeTypeAlias)=0 and name()='{0}'))";

                    sb.Append(string.Format(nodeTypeAlias, field));
                    sb.Append(" or ");
                }
                sb.Remove(sb.Length - 4, 4); //remove last " or "
                sb.Append(")");
            }

            //create the xpath statement to match all children of the current node.
            if (IndexerData.ParentNodeId.HasValue && IndexerData.ParentNodeId.Value > 0)
            {
                if (sb.Length > 0)
                    sb.Append(" and ");
                sb.Append("(");
                sb.Append("contains(@path, '," + IndexerData.ParentNodeId.Value + ",')"); //if the path contains comma - id - comma then the nodes must be a child
                sb.Append(")");
            }

            //create the full xpath statement to match the appropriate nodes. If there is a filter
            //then apply it, otherwise just select all nodes.
            var filter = sb.ToString();
            xPath = string.Format(xPath, filter.Length > 0 ? " and " + filter : "");

            //raise the event and set the xpath statement to the value returned
            var args = new IndexingNodesEventArgs(IndexerData, xPath, type);
            OnNodesIndexing(args);
            if (args.Cancel)
            {
                return;
            }

            xPath = args.XPath;

            DataService.LogService.AddVerboseLog(-1, string.Format("({0}) PerformIndexAll with XPATH: {1}", this.Name, xPath));

            AddNodesToQueue(xPath, type);
        }

        protected void AddNodesToQueue(IEnumerable<XElement> nodes, string type)
        {
            foreach (var node in nodes)
            {
                DataService.LogService.AddVerboseLog((int)node.Attribute("id"), string.Format("AddSingleNodeToIndex with type: {0}", type));

                int nodeId = int.Parse(node.Attribute(XName.Get("id")).Value);

                var values = GetDataToIndex(node, type);
                //raise the event and assign the value to the returned data from the event
                var indexingNodeDataArgs = new IndexingNodeDataEventArgs(node, nodeId, values, type);
                OnGatheringNodeData(indexingNodeDataArgs);
                values = indexingNodeDataArgs.Fields;

                // Determine if this is content or media
                if (type.ToFirstUpper() == PublishedItemType.Content.ToString())
                {
                    values.TryGetValue("nodeName", out string nodeName);
                    values.TryGetValue("updateDate", out string publishDate);
                    values.TryGetValue("urlName", out string urlName);

                    // Get content based on content fields in order of priority
                    string content = string.Empty;
                    if (values.TryGetValue("preamble", out string preamble))
                    {
                        content = preamble;
                    }
                    else if (values.TryGetValue("mainContent", out string mainContent))
                    {
                        content = mainContent;
                    }
                    else if (values.TryGetValue("calloutCards", out string calloutCards))
                    {
                        content = calloutCards;
                    }

                    // index the node
                    _searchService.UpdateIndex(nodeId, nodeName, DateTime.Parse(publishDate), urlName, content);
                }
                else if (type.ToFirstUpper() == PublishedItemType.Media.ToString())
                {
                    values.TryGetValue("nodeName", out string nodeName);
                    values.TryGetValue("updateDate", out string publishDate);
                    values.TryGetValue("urlName", out string urlName);
                    values.TryGetValue(Umbraco.Core.Constants.Conventions.Media.File, out string filePath);
                    values.TryGetValue(Umbraco.Core.Constants.Conventions.Media.Extension, out string nodeExtension);
                    values.TryGetValue(Umbraco.Core.Constants.Conventions.Media.Bytes, out string nodeBytes);

                    

                    // We only support indexing PDFs
                    if (nodeExtension == "pdf")
                    {
                        // index the node
                        _searchService.UpdateIndex(nodeId, nodeName, DateTime.Parse(publishDate), urlName, filePath, nodeExtension, nodeBytes);
                    }
                    else
                    {
                        DataService.LogService.AddVerboseLog((int)node.Attribute("id"), string.Format("Index only supports PDF files"));
                        LogHelper.Info<SearchService>("Media name " + nodeName + " with ID " + nodeId + " has not been pushed up to SQS. Reason: File type is not a PDF");
                    }

                }

                
            }
        }

        private void AddNodesToQueue(string xPath, string type)
        {
            // Get all the nodes of nodeTypeAlias == nodeTypeAlias
            XDocument xDoc = GetXDocument(xPath, type);
            if (xDoc != null)
            {
                XElement rootNode = xDoc.Root;

                IEnumerable<XElement> children = rootNode.Elements();

                AddNodesToQueue(children, type);
            }

        }
    }
}
