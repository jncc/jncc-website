using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Configuration;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.PropertyValueConverters
{
    [PropertyValueType(typeof(IEnumerable<int>))]
    [PropertyValueCache(PropertyCacheValue.All, PropertyCacheLevel.Content)]
    public class RadioButtonValueConverter : PropertyValueConverterBase
    {
        private readonly UmbracoHelper _umbracoHelper;

        public RadioButtonValueConverter()
            : this(new UmbracoHelper(UmbracoContext.Current))

        { }

        public RadioButtonValueConverter(UmbracoHelper umbracoHelper)
        {
            _umbracoHelper = umbracoHelper;
        }

        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            if (UmbracoConfig.For.UmbracoSettings().Content.EnablePropertyValueConverters)
            {
                return propertyType.PropertyEditorAlias.InvariantEquals(Umbraco.Core.Constants.PropertyEditors.RadioButtonListAlias);
            }
            return false;
        }

        public override object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
        {
            return source != null ? _umbracoHelper.GetPreValueAsString((int)source) : string.Empty;
        }

        public Type GetPropertyValueType(PublishedPropertyType propertyType)
        {
            return typeof(string);
        }

        private static readonly ConcurrentDictionary<int, bool> Storages = new ConcurrentDictionary<int, bool>();

        internal static void ClearCaches()
        {
            Storages.Clear();
        }
    }
}
