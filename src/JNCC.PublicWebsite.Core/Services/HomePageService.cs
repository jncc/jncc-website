using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Linq;
using Umbraco.Core;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class HomePageService
    {
        public HomePageViewModel GetViewModel(HomePage content)
        {
            var viewModel = new HomePageViewModel()
            {
                Carousel = GetCarouselViewModel(content)
            };

            return viewModel;
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
