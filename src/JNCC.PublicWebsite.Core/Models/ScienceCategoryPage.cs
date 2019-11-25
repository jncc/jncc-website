using JNCC.PublicWebsite.Core.Extensions;
using System.Collections.Generic;
using Umbraco.ModelsBuilder;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Models
{
    public partial class ScienceCategoryPage : IScienceCategorisablePage
    {
        ///<summary>
        /// Related Categories: Allows editors to related categories to the current category.  This is a one way process, if the linked category is also related to this one, it must be authored on the other category page too.
        ///</summary>
        [ImplementPropertyType("relatedCategories")]
        public IEnumerable<ScienceCategoryPage> RelatedCategories
        {
            get { return this.GetPropertyValueOfTypeOrDefault<ScienceCategoryPage>("relatedCategories"); }
        }

        ///<summary>
        /// Main Content: The main content for the page. Each section added here will be used to create an entry in the table of contents.  If no sections are authored then no table of content will be created.
        ///</summary>
        [ImplementPropertyType("mainContent")]
        public IEnumerable<ScienceCategorySectionBaseSchema> MainContent
        {
            get { return this.GetPropertyValueOfTypeOrDefault<ScienceCategorySectionBaseSchema>("mainContent"); }
        }

        ///<summary>
		/// Image and Text Section: Used to display an Image and Text section between the Preamble and the Contents listing block.
		///</summary>
        [ImplementPropertyType("imageAndTextSection")]
        public IEnumerable<ScienceCategorySectionBaseSchema> ImageAndTextSection
        {
            get { return this.GetPropertyValueOfTypeOrDefault<ScienceCategorySectionBaseSchema>("imageAndTextSection"); }
        }
    }
}
