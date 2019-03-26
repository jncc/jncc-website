using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class IFramePageViewModel
    {
        public IEnumerable<NavigationItemViewModel> Navigation { get; set; }
        public string SourceUrl { get; set; }
    }
}
