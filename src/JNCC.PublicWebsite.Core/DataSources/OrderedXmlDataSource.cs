using nuPickers.Shared.DotNetDataSource;
using System.Collections.Generic;
using System.Linq;
using umbraco;

namespace JNCC.PublicWebsite.Core.DataSources
{
    public sealed class OrderedXmlDataSource : IDotNetDataSource
    {
        [DotNetDataSource(Title = "XPath", Description = "The XPath statement to query the Umbraco Published Content XML cache")]
        public string XPath { get; set; }
        [DotNetDataSource(Title = "Key XPath", Description = "XPath (from each matched node) to select a single value for use as the key (this must be unique)")]
        public string KeyXPath { get; set; }
        [DotNetDataSource(Title = "Label XPath", Description = "XPath (from each matched node) to select a single value for use as the label")]
        public string LabelXPath { get; set; }

        public IEnumerable<KeyValuePair<string, string>> GetEditorDataItems(int contextId)
        {
            var xml = uQuery.GetPublishedXml(uQuery.UmbracoObjectType.Document);
            var xpathNodeIterator = xml.CreateNavigator().Select(XPath);
            var dataItems = new Dictionary<string, string>();

            while (xpathNodeIterator.MoveNext())
            {
                if (xpathNodeIterator.CurrentPosition > 1 || !(xpathNodeIterator.Current.GetAttribute("id", string.Empty) == "-1"))
                {
                    var key = xpathNodeIterator.Current.SelectSingleNode(KeyXPath).Value;
                    if (string.IsNullOrWhiteSpace(key) == false && dataItems.ContainsKey(key) == false)
                    {
                        var str = xpathNodeIterator.Current.SelectSingleNode(LabelXPath).Value;
                        dataItems.Add(key, str);
                    }
                }
            }

            return dataItems.OrderBy(x => x.Value).ToArray();
        }
    }
}
