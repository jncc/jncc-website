using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class HomePageViewModel : IHasCalloutCardsViewModel, IHasLatestNewsSectionViewModel
    {
        public CarouselViewModel Carousel { get; set; }
        public IEnumerable<CalloutCardViewModel> CalloutCards { get; set; }
        public string ResourcesTitle { get; set; }
        public IEnumerable<ResourceItemViewModel> ResourcesItems { get; set; }
        public LatestNewsSectionViewModel LatestNewsSection { get; set; }
    }
}
