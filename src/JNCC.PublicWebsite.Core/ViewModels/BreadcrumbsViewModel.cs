using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class BreadcrumbsViewModel
    {
        public IEnumerable<NavigationItemViewModel> Ancestors { get; set; }
        public string CurrentPage { get; set; }
    }
}
