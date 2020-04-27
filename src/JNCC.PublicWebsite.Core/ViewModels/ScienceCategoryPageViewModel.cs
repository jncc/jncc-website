using JNCC.PublicWebsite.Core.Utilities;
using System.Collections.Generic;
using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceCategoryPageViewModel
    {
        public string Headline { get; set; }
        public IHtmlString Preamble { get; set; }
        public IEnumerable<ScienceCategorySectionViewModel> Sections { get; set; }
        public IEnumerable<ScienceCategorySectionViewModel> ImageTextSection { get; set; }
        public bool HasSections
        {
            get
            {
                return ExistenceUtility.IsNullOrEmpty(Sections) == false;
            }
        }
        public IReadOnlyDictionary<char, IEnumerable<NavigationItemViewModel>> CategorisedPages { get; set; }
        public bool HasCategorisedPages
        {
            get
            {
                return ExistenceUtility.IsNullOrEmpty(CategorisedPages) == false;
            }
        }
        public IReadOnlyDictionary<char, IEnumerable<NavigationItemViewModel>> RelatedCategories { get; set; }
        public bool HasRelatedCategories
        {
            get
            {
                return ExistenceUtility.IsNullOrEmpty(RelatedCategories) == false;
            }
        }

        public bool TurnOffAZSection { get; set; }
    }
}