using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class HomePageService
    {
        private readonly NavigationItemService _navigationItemService;
        private readonly CalloutCardsService _calloutCardsService;
        private readonly LatestNewsSectionService _latestNewsSectionService;

        public HomePageService(CalloutCardsService calloutCardsService, NavigationItemService navigationItemService, LatestNewsSectionService latestNewsSectionService)
        {
            _calloutCardsService = calloutCardsService;
            _navigationItemService = navigationItemService;
            _latestNewsSectionService = latestNewsSectionService;
        }

        public HomePageViewModel GetViewModel(HomePage content)
        {
            var viewModel = new HomePageViewModel()
            {
                Carousel = GetCarouselViewModel(content),
                CalloutCards = _calloutCardsService.GetCalloutCards(content.CalloutCards),
                ResourcesTitle = content.ResourcesTitle,
                ResourcesItems = GetResourcesItems(content),
                LatestNewsSection = _latestNewsSectionService.GetViewModel(content)
            };

            return viewModel;
        }

        private IEnumerable<ResourceItemViewModel> GetResourcesItems(HomePage content)
        {
            var viewModels = new List<ResourceItemViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(content.ResourcesItems))
            {
                return viewModels;
            }

            foreach (var item in content.ResourcesItems)
            {
                var viewModel = new ResourceItemViewModel()
                {
                    Content = item.Content,
                    ReadMoreButton = _navigationItemService.GetViewModel(item.Link)
                };

                if (item.Image != null)
                {
                    viewModel.ImageUrl = item.Image.Url;
                }

                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        private CarouselViewModel GetCarouselViewModel(IPageHeroCarouselComposition content)
        {
            if (ExistenceUtility.IsNullOrEmpty(content.HeroImages))
            {
                return null;
            }

            var viewModel = new CarouselViewModel()
            {
                ImageUrls = content.HeroImages.WhereNotNull().Select(x => x.Url),
                Headline = content.Headline,
                Text = content.HeroContent
            };

            return viewModel;
        }
    }
}
