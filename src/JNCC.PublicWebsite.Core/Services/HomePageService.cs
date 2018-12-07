using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class HomePageService
    {
        private readonly NavigationItemService _navigationItemService;

        public HomePageService(NavigationItemService navigationItemService)
        {
            _navigationItemService = navigationItemService;
        }

        public HomePageViewModel GetViewModel(HomePage content)
        {
            var viewModel = new HomePageViewModel()
            {
                Carousel = GetCarouselViewModel(content),
                CalloutCards = GetCalloutCards(content)
            };

            return viewModel;
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
                        Url = card.Image.Url,
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
