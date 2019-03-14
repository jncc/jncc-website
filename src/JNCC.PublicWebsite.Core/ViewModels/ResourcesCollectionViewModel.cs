using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ResourcesCollectionViewModel
    {
        public string Title { get; set; }
        public IEnumerable<CalloutCardViewModel> Resources { get; set; }
        public NavigationItemViewModel ReadMoreLink { get; set; }
    }
}