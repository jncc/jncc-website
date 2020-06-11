using JNCC.PublicWebsite.Core.Utilities;
using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class SidebarViewModel : BasicSidebarViewModel
    {
        public IEnumerable<NavigationItemViewModel> InThisSectionLinks { get; set; }
        public bool HasInThisSectionLinks
        {
            get
            {
                return ExistenceUtility.IsNullOrEmpty(InThisSectionLinks) == false;
            }
        }

        public string AlsoInLinksTitle { get; set; }
        public IEnumerable<NavigationItemViewModel> AlsoInLinks { get; set; }
        public bool HasAlsoInLinks
        {
            get
            {
                return ExistenceUtility.IsNullOrEmpty(AlsoInLinks) == false;
            }
        }

        public string CurrentPageUrl { get; set; }
    }
}
