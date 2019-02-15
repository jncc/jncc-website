using JNCC.PublicWebsite.Core.Extensions;
using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.ModelsBuilder;

namespace JNCC.PublicWebsite.Core.Models
{
    public partial class ScienceDetailsPage
    {
        ///<summary>
        /// Main Content: The main content for the page. Each section added here will be used to create an entry in the table of contents.  If no sections are authored then no table of content will be created.
        ///</summary>
        [ImplementPropertyType("mainContent")]
        public IEnumerable<ScienceDetailsSectionBaseSchema> MainContent
        {
            get { return this.GetPropertyValueOfTypeOrDefault<ScienceDetailsSectionBaseSchema>("mainContent"); }
        }
    }
}
