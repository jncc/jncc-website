using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class CategorisedFooterLinksViewModel
    {
        public string Heading { get; set; }
        public IEnumerable<NavigationItemViewModel> Links { get; set; }
    }
}
