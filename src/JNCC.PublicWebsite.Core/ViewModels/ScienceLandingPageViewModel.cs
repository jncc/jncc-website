using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceLandingPageViewModel : IHasCalloutCardsViewModel, IHasLatestNewsSectionViewModel
    {
        public IEnumerable<CalloutCardViewModel> CalloutCards { get; set; }
        public LatestNewsSectionViewModel LatestNewsSection { get; set; }
        public ScienceLatestUpdatesSectionViewModel LatestUpdates { get; set; }
        public IEnumerable<ResourcesCollectionViewModel> ResourcesCollections { get; set; }
    }
}
