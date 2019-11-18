using JNCC.PublicWebsite.Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.ModelsBuilder;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Models
{
    public partial class ScienceDetailsPage : IScienceCategorisablePage
    {
        ///<summary>
        /// Main Content: The main content for the page. Each section added here will be used to create an entry in the table of contents.  If no sections are authored then no table of content will be created.
        ///</summary>
        [ImplementPropertyType("mainContent")]
        public IEnumerable<ScienceDetailsSectionBaseSchema> MainContent
        {
            get { return this.GetPropertyValueOfTypeOrDefault<ScienceDetailsSectionBaseSchema>("mainContent"); }
        }

        ///<summary>
        /// Categories
        ///</summary>
        [ImplementPropertyType("categories")]
        public IEnumerable<ScienceCategoryPage> Categories
        {
            get { return this.GetPropertyValueOfTypeOrDefault<ScienceCategoryPage>("categories"); }
        }

        ///<summary>
		/// Image and Text Section: Used to display an Image and Text section between the Preamble and the Contents listing block.
		///</summary>
		[ImplementPropertyType("imageAndTextSection")]
        public IEnumerable<ScienceDetailsSectionBaseSchema> ImageAndTextSection
        {
            get { return this.GetPropertyValueOfTypeOrDefault<ScienceDetailsSectionBaseSchema>("imageAndTextSection"); }
        }
    }
}
