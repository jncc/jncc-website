using Examine;
using JNCC.PublicWebsite.Core.Configuration;
using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using JNCC.PublicWebsite.Core.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using Umbraco.Core;
using Umbraco.Core.Configuration;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Routing;
using Umbraco.Web.Security;
using static Umbraco.Core.Constants;

namespace JNCC.PublicWebsite.Core.Indexers
{
    public class ElasticPublishedContentMediaIndexer : UmbracoExamine.UmbracoContentIndexer
    {
        private readonly ISearchConfiguration _searchConfiguration = SearchConfiguration.GetConfig();
        private const string _site = SearchIndexingSites.Website;

        public ElasticPublishedContentMediaIndexer() : base()
        {
            SupportedExtensions = new[] { "pdf" };
            UmbracoFileProperty = Conventions.Media.File;
            UmbracoExtensionProperty = Conventions.Media.Extension;
        }

        // Let Umbraco know that we support Content and Media
        private static List<string> _supportedTypes = new List<string>() { UmbracoExamine.IndexTypes.Content, UmbracoExamine.IndexTypes.Media };

        protected override IEnumerable<string> SupportedTypes => _supportedTypes;

        // This is to support multiple file extensions
        public IEnumerable<string> SupportedExtensions { get; set; }

        public string UmbracoFileProperty { get; set; }
        public string UmbracoExtensionProperty { get; set; }

        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);

            if (!string.IsNullOrEmpty(config["extensions"]))
                SupportedExtensions = config["extensions"].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            //checks if a custom field alias is specified
            if (!string.IsNullOrEmpty(config["umbracoFileProperty"]))
                UmbracoFileProperty = config["umbracoFileProperty"];

            //checks if a custom field alias is specified
            if (!string.IsNullOrEmpty(config["umbracoExtensionProperty"]))
                UmbracoExtensionProperty = config["umbracoExtensionProperty"];
        }

        public override void ReIndexNode(XElement node, string type)
        {
            if (_searchConfiguration.EnableIndexing == false)
            {
                LogHelper.Info<ElasticPublishedContentMediaIndexer>("Skipping ReIndexNode. EnableIndexing is configured to disabled.");
                return;
            }

            // Check to make sure it's a supported type (Content or Media)
            if (!_supportedTypes.Contains(type))
                return;

            // Check if type is a content page and has UmbracoNaviHide property
            if (node.Element(Conventions.Content.NaviHide) != null && type.ToFirstUpper() == PublishedItemType.Content.ToString())
            {
                // Check if content page is hidden
                if ((bool)node.Element(Conventions.Content.NaviHide))
                {
                    DeleteFromIndex(node.Attribute("id").Value);
                    return;
                }
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
            if (_searchConfiguration.EnableIndexing == false)
            {
                LogHelper.Info<ElasticPublishedContentMediaIndexer>("Skipping AddNodesToQueue. EnableIndexing is configured to disabled.");
                return;
            }

            var umbracoContext = GetUmbracoContext();

            if (umbracoContext == null)
            {
                LogHelper.Info<ElasticPublishedContentMediaIndexer>(() => "Skipping AddNodesToQueue as a valid UmbracoContext was unobtainable.");
                return;
            }

            var fullUrlResolverService = new UmbracoContextFullUrlResolverService(umbracoContext);

            using (var searchIndexingQueueService = new SearchIndexingQueueService(_searchConfiguration))
            {
                LogHelper.Info<ElasticPublishedContentMediaIndexer>("AddNodesToQueue: {0} node(s) for index type {1}.", () => nodes.Count(), () => type);

                foreach (var node in nodes)
                {
                    DataService.LogService.AddVerboseLog((int)node.Attribute("id"), string.Format("AddSingleNodeToIndex with type: {0}", type));

                    int nodeId = int.Parse(node.Attribute(XName.Get("id")).Value);

                    var values = GetDataToIndex(node, type);
                    //raise the event and assign the value to the returned data from the event
                    var indexingNodeDataArgs = new IndexingNodeDataEventArgs(node, nodeId, values, type);
                    OnGatheringNodeData(indexingNodeDataArgs);
                    values = indexingNodeDataArgs.Fields;

                    values.TryGetValue("nodeName", out string nodeName);
                    values.TryGetValue("updateDate", out string publishDate);

                    var document = new SearchIndexDocumentModel()
                    {
                        NodeId = nodeId,
                        Site = _site,
                        Published = DateTime.Parse(publishDate),
                        Title = nodeName
                    };

                    // Determine if this is content or media
                    if (string.Equals(type, UmbracoExamine.IndexTypes.Content, StringComparison.OrdinalIgnoreCase))
                    {
                        var url = "#";// fullUrlResolverService.ResolveContentFullUrlById(nodeId);

                        // Get content based on content fields in order of priority
                        var contentBuilder = new StringBuilder();

                        foreach (var contentField in values.Where(x => IndexerData.UserFields.Select(y => y.Name).Contains(x.Key)))
                        {
                            var contentFieldValue = contentField.Value;
                            // Check if it has a value and append it
                            if (string.IsNullOrEmpty(contentFieldValue) == false)
                            {
                                if (contentFieldValue.DetectIsJson() && JsonUtility.TryParseJson(contentFieldValue, out object parsedJson))
                                {
                                    var processedJsonValue = ProcessJsonValue(parsedJson);

                                    if (string.IsNullOrEmpty(processedJsonValue) == false)
                                    {
                                        contentBuilder.AppendLine(processedJsonValue);
                                    }
                                }
                                else
                                {
                                    var sanitisedValue = contentFieldValue.StripHtml().Trim();

                                    if (string.IsNullOrWhiteSpace(sanitisedValue) == false)
                                    {
                                        contentBuilder.AppendLine(sanitisedValue);
                                    }
                                }
                            }
                        }


                        string content = contentBuilder.ToString().Trim();

                        document.Url = url;
                        document.Content = content;

                        // index the node
                        searchIndexingQueueService.QueueUpsert(document);
                    }
                    else if (string.Equals(type, UmbracoExamine.IndexTypes.Media, StringComparison.OrdinalIgnoreCase))
                    {
                        var fileExtension = node.Elements().FirstOrDefault(x =>
                        {
                            if (x.Attribute("alias") != null)
                            {
                                return (string)x.Attribute("alias") == this.UmbracoExtensionProperty;
                            }
                            else
                            {
                                return x.Name == this.UmbracoExtensionProperty;
                            }
                        });

                        if (HasNode(fileExtension) == false)
                        {
                            LogHelper.Warn<ElasticPublishedContentMediaIndexer>("Media name " + nodeName + " with ID " + nodeId + " has not been pushed up to SQS. Reason: " + UmbracoExtensionProperty + " value was not present.");
                            continue;
                        }

                        if (!SupportedExtensions.Contains(fileExtension.Value, StringComparer.OrdinalIgnoreCase))
                        {
                            LogHelper.Info<ElasticPublishedContentMediaIndexer>("Media name " + nodeName + " with ID " + nodeId + " has not been pushed up to SQS. Reason: File extension, " + fileExtension.Value + ", is not supported.");
                            continue;
                        }

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


                        if (HasNode(filePath) == false)
                        {
                            LogHelper.Warn<ElasticPublishedContentMediaIndexer>("Media name " + nodeName + " with ID " + nodeId + " has not been pushed up to SQS. Reason: " + UmbracoFileProperty + " value was not present.");
                            continue;
                        }

                        //get the file path from the data service
                        var fullPath = this.DataService.MapPath((string)filePath);

                        if (System.IO.File.Exists(fullPath) == false)
                        {
                            LogHelper.Warn<ElasticPublishedContentMediaIndexer>("Media name " + nodeName + " with ID " + nodeId + " has not been pushed up to SQS. Reason: Physical file does not exist.");
                            continue;
                        }

                        var fileInfo = new FileInfo(fullPath);
                        var url = fullUrlResolverService.ResolveMediaFullUrl(filePath.Value);
                        // index the node

                        var pdf = System.IO.File.ReadAllBytes(fullPath);
                        var pdfEncoded = Convert.ToBase64String(pdf);

                        document.Url = url;
                        document.Content = "Umbraco Media File";
                        document.FileBase64Encoded = pdfEncoded;
                        document.FileExtension = fileInfo.Extension;
                        document.FileSizeInBytes = fileInfo.Length;

                        searchIndexingQueueService.QueueUpsert(document);
                    }
                }
            }
        }

        private UmbracoContext GetUmbracoContext()
        {
            try
            {
                if (HttpContext.Current == null || HttpContext.Current.Request == null)
                {
                    LogHelper.Info<ElasticPublishedContentMediaIndexer>(() => "Skipped creating UmbracoContext as HttpContext is unavailable.");
                    return null;
                }

                LogHelper.Info<ElasticPublishedContentMediaIndexer>(() => "Creating new UmbracoContext using UmbracoContext.EnsureContext.");

                var httpContext = new HttpContextWrapper(HttpContext.Current);

                return UmbracoContext.EnsureContext(httpContext,
                                                    ApplicationContext.Current,
                                                    new WebSecurity(httpContext, ApplicationContext.Current),
                                                    UmbracoConfig.For.UmbracoSettings(),
                                                    UrlProviderResolver.Current.Providers,
                                                    false);
            }
            catch (HttpException ex)
            {
                LogHelper.Error<ElasticPublishedContentMediaIndexer>("Skipped creating UmbracoContext as HttpContext is unavailable.", ex);
                return null;
            }
        }

        private string ProcessJsonValue(object obj)
        {
            var processedValue = new StringBuilder();
            var objType = obj.GetType();

            if (objType == typeof(JObject))
            {
                var jObj = obj as JObject;
                if (jObj != null)
                {
                    foreach (var field in _searchConfiguration.NestedIndexFields)
                    {
                        var nestedField = jObj[field.Alias];
                        if (nestedField == null)
                        {
                            continue;
                        }

                        var valueType = nestedField.GetType();
                        var value = nestedField.Value<string>();

                        if (typeof(JContainer).IsAssignableFrom(valueType))
                        {
                            var innerValue = ProcessJsonValue(value);

                            if (string.IsNullOrWhiteSpace(innerValue) == false)
                            {
                                processedValue.AppendLine(innerValue);
                            }
                        }
                        else
                        {
                            var sanitisedValue = value.StripHtml().Trim();

                            if (string.IsNullOrWhiteSpace(sanitisedValue) == false)
                            {
                                processedValue.AppendLine(sanitisedValue);
                            }
                        }
                    }
                }
            }
            else if (objType == typeof(JArray))
            {
                var jArr = obj as JArray;
                if (jArr != null)
                {
                    for (var i = 0; i < jArr.Count; i++)
                    {
                        var item = jArr[i];

                        foreach (var field in _searchConfiguration.NestedIndexFields)
                        {
                            var nestedField = item[field.Alias];

                            if (nestedField == null)
                            {
                                continue;
                            }

                            var valueType = nestedField.GetType();
                            var value = nestedField.Value<string>();

                            if (typeof(JContainer).IsAssignableFrom(valueType))
                            {
                                var innerValue = ProcessJsonValue(value);

                                if (string.IsNullOrWhiteSpace(innerValue) == false)
                                {
                                    processedValue.AppendLine(innerValue);
                                }
                            }
                            else
                            {
                                var sanitisedValue = value.StripHtml().Trim();

                                if (string.IsNullOrWhiteSpace(sanitisedValue) == false)
                                {
                                    processedValue.AppendLine(sanitisedValue);
                                }
                            }
                        }
                    }
                }
            }

            return processedValue.ToString().Trim();
        }

        public override void RebuildIndex()
        {
            if (_searchConfiguration.EnableIndexing == false)
            {
                LogHelper.Info<ElasticPublishedContentMediaIndexer>("Skipping RebuildIndex. EnableIndexing is configured to disabled.");
                PostRebuildIndexCleanUp();
                return;
            }

            LogHelper.Info<ElasticPublishedContentMediaIndexer>("Rebuild Elastic Index started");
            base.RebuildIndex();
            PostRebuildIndexCleanUp();
            LogHelper.Info<ElasticPublishedContentMediaIndexer>("Rebuild Elastic Index completed");
        }

        private void PostRebuildIndexCleanUp()
        {
            var cacheKey = "temp_indexing_op_" + Name;

            if (ApplicationContext.Current == null)
            {
                LogHelper.Warn<ElasticPublishedContentMediaIndexer>("Unable to perform post-Rebuild Index clean up");
                return;
            }

            LogHelper.Info<ElasticPublishedContentMediaIndexer>("Post-Rebuild Index clean up completed");
            ApplicationContext.Current.ApplicationCache.RuntimeCache.ClearCacheItem(cacheKey);
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
            //if ((bool)node.Element(Conventions.Content.NaviHide))
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
            if (_searchConfiguration.EnableIndexing == false)
            {
                LogHelper.Info<ElasticPublishedContentMediaIndexer>("Skipping DeleteFromIndex. EnableIndexing is configured to disabled.");
                return;
            }

            var internalSearcher = ExamineManager.Instance.SearchProviderCollection["InternalSearcher"];
            var parsedNodeId = int.Parse(nodeId);
            var descendantPath = string.Format(@"\-1\,*{0}\,*", parsedNodeId);
            var rawQuery = string.Format("{0}:{1}", IndexPathFieldName, descendantPath);
            var c = internalSearcher.CreateSearchCriteria();
            var filtered = c.RawQuery(rawQuery);
            var descendants = internalSearcher.Search(filtered);

            DataService.LogService.AddVerboseLog(parsedNodeId, string.Format("DeleteFromIndex with query: {0} (found {1} descandant(s))", rawQuery, descendants.Count()));

            var document = new SearchIndexDocumentModel()
            {
                NodeId = parsedNodeId,
                Site = _site
            };

            using (var searchIndexingQueueService = new SearchIndexingQueueService(_searchConfiguration))
            {
                searchIndexingQueueService.QueueDelete(document);

                //need to create a delete queue item for each one found
                foreach (var node in descendants)
                {
                    var descendantDocument = new SearchIndexDocumentModel()
                    {
                        NodeId = node.Id,
                        Site = _site
                    };

                    searchIndexingQueueService.QueueDelete(document);
                }
            }
        }

        private static bool HasNode(XElement node)
        {
            return node != default(XElement) && !string.IsNullOrEmpty((string)node);
        }
    }
}
