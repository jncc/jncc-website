using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class ScienceLandingPageService
    {
        private const int NumberOfLatestUpdatesItems = 3;
        private const int LatestUpdateItemContentLength = 75;

        private readonly HtmlStringUtilities _htmlStringUtilities;
        private readonly CalloutCardsService _calloutCardsService;
        private readonly LatestNewsSectionService _latestNewsSectionService;
        private readonly NavigationItemService _navigationItemService;

        public ScienceLandingPageService(CalloutCardsService calloutCardsService, LatestNewsSectionService latestNewsSectionService, NavigationItemService navigationItemService)
        {
            _htmlStringUtilities = new HtmlStringUtilities();
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
                LatestNewsSection = _latestNewsSectionService.GetViewModel(homePage),
                LatestUpdates = GetLatestUpdates(model)
            };
        }

        private ScienceLatestUpdatesSectionViewModel GetLatestUpdates(ScienceLandingPage model)
        {
            var viewModel = new ScienceLatestUpdatesSectionViewModel()
            {
                Pages = GetLatestUpdatedPages(model)
            };

            return viewModel;
        }

        private IEnumerable<ScienceLatestUpdatedPageItemViewModel> GetLatestUpdatedPages(ScienceLandingPage model)
        {
            var viewModels = new List<ScienceLatestUpdatedPageItemViewModel>();
            var pages = model.Children<ScienceDetailsPage>();

            if (ExistenceUtility.IsNullOrEmpty(pages))
            {
                return viewModels;
            }

            var latestPages = pages.OrderByDescending(x => x.UpdateDate)
                                   .Take(NumberOfLatestUpdatesItems)
                                   .ToArray();

            foreach (var page in latestPages)
            {
                var viewModel = new ScienceLatestUpdatedPageItemViewModel()
                {
                    Title = page.GetHeadline(),
                    ReadMoreLink = _navigationItemService.GetViewModel(page)
                };

                if (ExistenceUtility.IsNullOrWhiteSpace(page.Preamble) == false)
                {
                    viewModel.Content = _htmlStringUtilities.Truncate(page.Preamble.ToHtmlString(), LatestUpdateItemContentLength, true, false);
                }

                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}
