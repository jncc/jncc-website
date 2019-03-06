using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class ScienceLandingPageService
    {
        private readonly CalloutCardsService _calloutCardsService;
        private readonly LatestNewsSectionService _latestNewsSectionService;
        private readonly NavigationItemService _navigationItemService;

        public ScienceLandingPageService(CalloutCardsService calloutCardsService, LatestNewsSectionService latestNewsSectionService, NavigationItemService navigationItemService)
        {
            _calloutCardsService = calloutCardsService;
            _latestNewsSectionService = latestNewsSectionService;
            _navigationItemService = navigationItemService;
        }

        public ScienceLandingPageViewModel GetViewModel(ScienceLandingPage model)
        {
            var homePage = model.Site<HomePage>();

            return new ScienceLandingPageViewModel()
            {
                CalloutCards = _calloutCardsService.GetCalloutCards(model.CalloutCards),
                LatestNewsSection = _latestNewsSectionService.GetViewModel(homePage)
            };
        }
    }
}
