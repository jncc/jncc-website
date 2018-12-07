using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.ModelsBuilder;
using Umbraco.Web;
using JNCC.PublicWebsite.Core.Extensions;

namespace JNCC.PublicWebsite.Core.Models
{
    public partial class HomePage
    {
        ///<summary>
        /// Categorised Links: Links grouped into useful categories (e.g. About, Our Services, etc) to be displayed within the footer below the main content of the page.
        ///</summary>
        [ImplementPropertyType("footerCategorisedLinks")]
        public IEnumerable<CategorisedFooterLinksSchema> FooterCategorisedLinks
        {
            get { return this.GetPropertyValue<IEnumerable<IPublishedContent>>("footerCategorisedLinks")
                             .OfType<CategorisedFooterLinksSchema>(); }
        }

        ///<summary>
        /// Callout Cards: Boxes for important information which will appear below the hero carousel.
        ///</summary>
        [ImplementPropertyType("calloutCards")]
        public IEnumerable<CalloutCardSchema> CalloutCards
        {
            get { return this.GetPropertyValueOfTypeOrDefault<CalloutCardSchema>("calloutCards"); }
        }
    }
}
