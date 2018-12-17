using JNCC.PublicWebsite.Core.Utilities;
using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class SidebarViewModel
    {
        public NavigationItemViewModel PrimaryCallToActionButton { get; set; }

        public IEnumerable<NavigationItemViewModel> InThisSectionLinks { get; set; }
        public bool HasInThisSectionLinks
        {
            get
            {
                return ExistenceUtility.IsNullOrEmpty(InThisSectionLinks) == false;
            }
        }

        public IEnumerable<NavigationItemViewModel> SeeAlsoLinks { get; set; }
        public bool HasSeeAlsoLinks
        {
            get
            {
                return ExistenceUtility.IsNullOrEmpty(SeeAlsoLinks) == false;
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
    }
}
