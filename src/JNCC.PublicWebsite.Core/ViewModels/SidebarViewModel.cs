using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public class SidebarViewModel
    {
        public NavigationItemViewModel GetInTouchButton { get; set; }

        public IEnumerable<NavigationItemViewModel> InThisSectionLinks { get; set; }

        public IEnumerable<NavigationItemViewModel> SeeAlsoLinks { get; set; }
    }
}
