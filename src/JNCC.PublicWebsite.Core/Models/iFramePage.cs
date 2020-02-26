using JNCC.PublicWebsite.Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.ModelsBuilder;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Models
{
    public partial class IFramePage
    {
        ///<summary>
        /// Fallback Content: This content is a text alternative for the mapper page, this will be displayed on the devices set by the toggle switches below
        ///</summary>
        [ImplementPropertyType("fallbackContent")]
        public IEnumerable<ScienceIframeSectionBaseSchema> FallbackContent
        {
            get { return this.GetPropertyValueOfTypeOrDefault<ScienceIframeSectionBaseSchema>("fallbackContent"); }
        }
    }
}
