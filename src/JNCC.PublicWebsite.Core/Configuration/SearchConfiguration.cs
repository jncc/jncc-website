using System.Collections.Generic;
using System.Configuration;

namespace JNCC.PublicWebsite.Core.Configuration
{
    public sealed class SearchConfiguration : ConfigurationSection, ISearchConfiguration
    {
        [ConfigurationProperty("AWSESAccessKey")]
        public string AWSESAccessKey { get { return (string)this["AWSESAccessKey"]; } }

        [ConfigurationProperty("AWSESSecretKey")]
        public string AWSESSecretKey { get { return (string)this["AWSESSecretKey"]; } }

        [ConfigurationProperty("AWSSQSAccessKey")]
        public string AWSSQSAccessKey { get { return (string)this["AWSSQSAccessKey"]; } }

        [ConfigurationProperty("AWSSQSSecretKey")]
        public string AWSSQSSecretKey { get { return (string)this["AWSSQSSecretKey"]; } }

        [ConfigurationProperty("AWSESRegion")]
        public string AWSESRegion { get { return (string)this["AWSESRegion"]; } }

        [ConfigurationProperty("AWSService")]
        public string AWSService { get { return (string)this["AWSService"]; } }

        [ConfigurationProperty("AWSESEndpoint")]
        public string AWSESEndpoint { get { return (string)this["AWSESEndpoint"]; } }

        [ConfigurationProperty("AWSESIndex")]
        public string AWSESIndex { get { return (string)this["AWSESIndex"]; } }

        [ConfigurationProperty("AWSSQSEndpoint")]
        public string AWSSQSEndpoint { get { return (string)this["AWSSQSEndpoint"]; } }

        [ConfigurationProperty("AWSSQSPayloadBucket")]
        public string AWSSQSPayloadBucket { get { return (string)this["AWSSQSPayloadBucket"]; } }

        [ConfigurationProperty("EnableIndexing")]
        public bool EnableIndexing { get { return (bool)this["EnableIndexing"]; } }

        [ConfigurationProperty("indexNestedFields")]
        public SearchIndexNestedFieldElementCollection NestedIndexFieldsCollection
        {
            get { return this["indexNestedFields"] as SearchIndexNestedFieldElementCollection; }
        }

        public IEnumerable<ISearchIndexNestedField> NestedIndexFields
        {
            get { return NestedIndexFieldsCollection; }
        }

        internal static SearchConfiguration GetConfig()
        {
            return ConfigurationManager.GetSection("searchConfig") as SearchConfiguration;
        }
    }
}
