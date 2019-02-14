using System.Configuration;

namespace JNCC.PublicWebsite.Core.Configuration
{
    public sealed class SearchIndexNestedFieldElement : ConfigurationElement, ISearchIndexNestedField
    {
        private const string aliasPropertyName = "alias";

        [ConfigurationProperty(aliasPropertyName, IsRequired = false)]
        public string Alias
        {
            get { return (string)base[aliasPropertyName]; }
        }
    }
}
