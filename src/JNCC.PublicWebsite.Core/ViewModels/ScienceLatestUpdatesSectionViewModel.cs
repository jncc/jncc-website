using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceLatestUpdatesSectionViewModel
    {
        public IEnumerable<ScienceLatestUpdatedPageItemViewModel> Pages { get; set; }
        public NavigationItemViewModel AToZPageLink { get; set; }
    }
}