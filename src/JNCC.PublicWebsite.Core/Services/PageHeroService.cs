using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using JNCC.PublicWebsite.Core.Utilities;
using Umbraco.Core.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class PageHeroService
    {
        public PageHeroViewModel GetViewModel(IPageHeroComposition pageHeroComposition)
        {
            if (pageHeroComposition.HeroImage == null)
            {
                return null;
            }

            var headline = string.IsNullOrWhiteSpace(pageHeroComposition.Headline) == false ?
                             pageHeroComposition.Headline
                           : pageHeroComposition.Name;

            return new PageHeroViewModel()
            {
                Headline = headline,
                ImageUrl = pageHeroComposition.HeroImage.Url
            };
        }

        public bool HasPageHero(IPublishedContent currentPage)
        {
            var isPageHeroComposition = currentPage is IPageHeroCarouselComposition;
            var isPageHeroCarouselComposition = currentPage is IPageHeroCarouselComposition;

            if (isPageHeroComposition == false && isPageHeroCarouselComposition == false)
            {
                return false;
            }

            if (isPageHeroComposition)
            {
                return (currentPage as IPageHeroComposition).HasPageHeroImage();
            }
            
            return ExistenceUtility.IsNullOrEmpty((currentPage as IPageHeroCarouselComposition).HeroImages) == false;
        }
    }
}
