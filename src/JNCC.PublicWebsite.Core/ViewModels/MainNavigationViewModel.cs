using JNCC.PublicWebsite.Core.Constants;
using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class MainNavigationViewModel
    {
        public IEnumerable<MainNavigationItemViewModel> Items { get; set; }
        public PageHeroAvailability PageHeroAvailability { get; set; }
    }
}
