using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class ScienceLandingPageService
    {
        private readonly CalloutCardsService _calloutCardsService;
        private readonly LatestNewsSectionService _latestNewsSectionService;

        public ScienceLandingPageService(CalloutCardsService calloutCardsService, LatestNewsSectionService latestNewsSectionService)
        {
            _calloutCardsService = calloutCardsService;
            _latestNewsSectionService = latestNewsSectionService;
        }

        public ScienceLandingPageViewModel GetViewModel(ScienceLandingPage model)
        {
            return new ScienceLandingPageViewModel()
            {
                CalloutCards = _calloutCardsService.GetCalloutCards(model.CalloutCards)
            };
        }
    }
}
