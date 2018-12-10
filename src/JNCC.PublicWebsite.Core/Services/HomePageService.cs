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
        private const string XPathForLatestNewsItems = "//ArticlePage[@isDoc]";
        private const int NumberOfLatestNewsItems = 2;

        private readonly NavigationItemService _navigationItemService;
        private readonly UmbracoHelper _umbracoHelper;

        public HomePageService(NavigationItemService navigationItemService, UmbracoHelper umbracoHelper)
        {
            _navigationItemService = navigationItemService;
            _umbracoHelper = umbracoHelper;
        }

        public HomePageViewModel GetViewModel(HomePage content)
        {
            var viewModel = new HomePageViewModel()
            {
                Carousel = GetCarouselViewModel(content),
                CalloutCards = GetCalloutCards(content),
                ResourcesTitle = content.ResourcesTitle,
                ResourcesItems = GetResourcesItems(content),
                LatestNews = GetLatestNewsItems(content)
            };

            return viewModel;
        }

        private IEnumerable<LatestNewsItemViewModel> GetLatestNewsItems(HomePage content)
        {
            var viewModels = new List<LatestNewsItemViewModel>();

            if (content.ShowLatestNews == false)
            {
                return viewModels;
            }

            var latestNewsItems = _umbracoHelper.TypedContentAtXPath(XPathForLatestNewsItems)
                                                .OfType<ArticlePage>()
                                                .OrderByDescending(x => x.PublishDate)
                                                .Take(NumberOfLatestNewsItems);

            if (ExistenceUtility.IsNullOrEmpty(latestNewsItems))
            {
                return viewModels;
            }

            foreach (var newsItem in latestNewsItems)
            {
                var viewModel = new LatestNewsItemViewModel
                {
                    Title = string.IsNullOrWhiteSpace(newsItem.Headline) ? newsItem.Name : newsItem.Headline,
                    PublishDate = newsItem.PublishDate,
                    Description = newsItem.LandingPageContent,
                    Url = newsItem.Url
                };

                if (newsItem.HeroImage != null)
                {
                    viewModel.ImageUrl = newsItem.HeroImage.GetCropUrl(ImageCropAliases.ListingThumbnail);
                }

                viewModels.Add(viewModel);
            }

            return viewModels;
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

        private IEnumerable<CalloutCardViewModel> GetCalloutCards(HomePage content)
        {
            var viewModels = new List<CalloutCardViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(content.CalloutCards))
            {
                return viewModels;
            }

            foreach (var card in content.CalloutCards)
            {
                var viewModel = new CalloutCardViewModel()
                {
                    Title = card.Title,
                    Content = card.Content,
                    ReadMoreButton = _navigationItemService.GetViewModel(card.ReadMoreButton)
                };

                if (card.Image != null)
                {
                    viewModel.Image = new ImageViewModel()
                    {
                        Url = card.Image.Image.Url,
                        AlternativeText = card.Image.ImageAlternativeText
                    };
                };

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
