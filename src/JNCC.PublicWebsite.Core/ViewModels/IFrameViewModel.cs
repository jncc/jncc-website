using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class IFrameViewModel
    {
        public IEnumerable<NavigationItemViewModel> Navigation { get; set; }
        public string SourceUrl { get; set; }
    }
}
