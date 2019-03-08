using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class CareersSidebarViewModel : BasicSidebarViewModel
    {
        public IEnumerable<NavigationItemViewModel> LatestJobs { get; set; }
    }
}
