using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class ScienceLandingPageService
    {
        private readonly CalloutCardsService _calloutCardsService;

        public ScienceLandingPageService(CalloutCardsService calloutCardsService)
        {
            _calloutCardsService = calloutCardsService;
        }

        public ScienceLandingPageViewModel GetViewModel(ScienceLandingPage model)
        {
            return new ScienceLandingPageViewModel();
        }
    }
}
