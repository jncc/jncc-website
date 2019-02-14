using System;
using System.Collections.Generic;
using System.Configuration;

namespace JNCC.PublicWebsite.Core.Configuration
{
    [ConfigurationCollection(typeof(SearchIndexNestedFieldElement))]
    public sealed class SearchIndexNestedFieldElementCollection : ConfigurationElementCollection, IEnumerable<ISearchIndexNestedField>
    {
        internal const string PropertyName = "field";

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }

        protected override string ElementName
        {
            get
            {
                return PropertyName;
            }
        }

        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(PropertyName, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool IsReadOnly()
        {
            return true;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new SearchIndexNestedFieldElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SearchIndexNestedFieldElement)(element)).Alias;
        }

        IEnumerator<ISearchIndexNestedField> IEnumerable<ISearchIndexNestedField>.GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return this[i] as ISearchIndexNestedField;
            }
        }

        public SearchIndexNestedFieldElement this[int index]
        {
            get { return (SearchIndexNestedFieldElement)BaseGet(index); }
        }
    }
}
