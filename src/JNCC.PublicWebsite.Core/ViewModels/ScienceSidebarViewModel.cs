using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceSidebarViewModel : BasicSidebarViewModel
    {
        public IEnumerable<MainNavigationItemViewModel> Categories { get; set; }
    }
}