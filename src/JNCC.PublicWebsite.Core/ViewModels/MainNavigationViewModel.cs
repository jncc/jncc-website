using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class MainNavigationViewModel
    {
        public IEnumerable<MainNavigationItemViewModel> Items { get; set; }
        public bool HasPageHero { get; set; }
    }
}
