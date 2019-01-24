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

        [ConfigurationProperty("AWSESRegion")]
        public string AWSESRegion { get { return (string)this["AWSESRegion"]; } }

        [ConfigurationProperty("AWSService")]
        public string AWSService { get { return (string)this["AWSService"]; } }

        [ConfigurationProperty("AWSESEndpoint")]
        public string AWSESEndpoint { get { return (string)this["AWSESEndpoint"]; } }

        [ConfigurationProperty("AWSESIndex")]
        public string AWSESIndex { get { return (string)this["AWSESIndex"]; } }

        internal static SearchConfiguration GetConfig()
        {
            return ConfigurationManager.GetSection("searchConfig") as SearchConfiguration;
        }
    }
}
