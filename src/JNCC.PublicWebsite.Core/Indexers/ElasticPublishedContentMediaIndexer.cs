using Examine;
using JNCC.PublicWebsite.Core.Configuration;
using JNCC.PublicWebsite.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
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
        public ElasticPublishedContentMediaIndexer() : base()
        {
            var config = SearchConfiguration.GetConfig();
            _searchService = new SearchService(config);
            SupportedExtensions = new[] { ".pdf" };
            UmbracoFileProperty = "umbracoFile";
        }

        private SearchService _searchService;

        // Let Umbraco know that we support Content and Media
        private static List<string> _supportedTypes = new List<string>() { UmbracoExamine.IndexTypes.Content, UmbracoExamine.IndexTypes.Media };

        protected override IEnumerable<string> SupportedTypes => _supportedTypes;

        // This is to support multiple file extensions
        public IEnumerable<string> SupportedExtensions { get; set; }

        public string UmbracoFileProperty { get; set; }

        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);

            if (!string.IsNullOrEmpty(config["extensions"]))
                SupportedExtensions = config["extensions"].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            //checks if a custom field alias is specified
            if (!string.IsNullOrEmpty(config["umbracoFileProperty"]))
                UmbracoFileProperty = config["umbracoFileProperty"];
        }

        public override void ReIndexNode(XElement node, string type)
        {
            // Check to make sure we're running on PRD-WEB
            //TODO: Check to make sure we're running on PRD-WEB

            // Check to make sure it's a supported type (Content or Media)
            if (!_supportedTypes.Contains(type))
                return;

            // Check if page is hidden
            if ((bool)node.Element(Umbraco.Core.Constants.Conventions.Content.NaviHide))
            {
                DeleteFromIndex(node.Attribute("id").Value);
                return;
            }

            AddSingleNodeToIndex(node, type);
        }

        protected override void AddSingleNodeToIndex(XElement node, string type)
        {
            DataService.LogService.AddVerboseLog((int)node.Attribute("id"), string.Format("AddSingleNodeToIndex with type: {0}", type));

            AddNodesToQueue(new XElement[] { node }, type);
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

                    var filePath = node.Elements().FirstOrDefault(x =>
                    {
                        if (x.Attribute("alias") != null)
                        {
                            return (string)x.Attribute("alias") == this.UmbracoFileProperty;
                        }
                        else
                        {
                            return x.Name == this.UmbracoFileProperty;
                        }
                    });
                    if (FileExists(filePath))
                    {
                        //get the file path from the data service
                        var fullPath = this.DataService.MapPath((string)filePath);
                        var fileInfo = new FileInfo(fullPath);

                        if (!SupportedExtensions.Select(x => x.ToUpper()).Contains(fileInfo.Extension.ToUpper()))
                        {
                            throw new NotSupportedException("The file with the extension specified is not supported");
                        }

                        // index the node
                        _searchService.UpdateIndex(nodeId, nodeName, DateTime.Parse(publishDate), filePath.Value, fullPath, fileInfo.Extension, fileInfo.Length.ToString());
                    }
                    else
                    {
                        DataService.LogService.AddVerboseLog((int)node.Attribute("id"), string.Format("Index only supports PDF files"));
                        LogHelper.Info<SearchService>("Media name " + nodeName + " with ID " + nodeId + " has not been pushed up to SQS. Reason: File type is not a PDF");
                    }
                }
            }
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

        private void AddNodesToQueue(string xPath, string type)
        {
            //TODO: Check if page is hidden
            //if ((bool)node.Element(Umbraco.Core.Constants.Conventions.Content.NaviHide))
            //    return;

            // Get all the nodes of nodeTypeAlias == nodeTypeAlias
            XDocument xDoc = GetXDocument(xPath, type);
            if (xDoc != null)
            {
                XElement rootNode = xDoc.Root;

                IEnumerable<XElement> children = rootNode.Elements();

                AddNodesToQueue(children, type);
            }
        }

        public override void DeleteFromIndex(string nodeId)
        {
            var internalSearcher = ExamineManager.Instance.SearchProviderCollection["InternalSearcher"];

            var descendantPath = string.Format(@"\-1\,*{0}\,*", nodeId);
            var rawQuery = string.Format("{0}:{1}", IndexPathFieldName, descendantPath);
            var c = internalSearcher.CreateSearchCriteria();
            var filtered = c.RawQuery(rawQuery);
            var descendants = internalSearcher.Search(filtered);

            DataService.LogService.AddVerboseLog(int.Parse(nodeId), string.Format("DeleteFromIndex with query: {0} (found {1} descandant(s))", rawQuery, descendants.Count()));

            _searchService.DeleteFromIndex(nodeId);

            //need to create a delete queue item for each one found
            foreach (var node in descendants)
            {
                _searchService.DeleteFromIndex(node.Id.ToString());
            }
        }

        private static bool FileExists(XElement filePath)
        {
            return filePath != default(XElement) && !string.IsNullOrEmpty((string)filePath);
        }
    }
}
