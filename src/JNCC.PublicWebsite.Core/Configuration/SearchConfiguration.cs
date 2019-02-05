using System;
using System.Configuration;

namespace JNCC.PublicWebsite.Core.Configuration
{
    public sealed class SearchConfiguration : ConfigurationSection, ISearchConfiguration
    {
        [ConfigurationProperty("AWSESAccessKey")]
        public string AWSESAccessKey { get { return (string)this["AWSESAccessKey"]; } }

        [ConfigurationProperty("AWSESSecretKey")]
        public string AWSESSecretKey { get { return (string)this["AWSESSecretKey"]; } }

        [ConfigurationProperty("AWSESWriteAccessKey")]
        public string AWSESWriteAccessKey { get { return (string)this["AWSESWriteAccessKey"]; } }

        [ConfigurationProperty("AWSESWriteSecretKey")]
        public string AWSESWriteSecretKey { get { return (string)this["AWSESWriteSecretKey"]; } }

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

        [ConfigurationProperty("IsMaster")]
        public bool IsMaster { get { return (bool)this["IsMaster"]; } }

        internal static SearchConfiguration GetConfig()
        {
            return ConfigurationManager.GetSection("searchConfig") as SearchConfiguration;
        }
    }
}
