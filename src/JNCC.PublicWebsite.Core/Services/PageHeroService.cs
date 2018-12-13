using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
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

        public PageHeroAvailability GetPageHeroAvailabilty(IPublishedContent currentPage)
        {
            if (currentPage is IPageHeroComposition == false)
            {
                return PageHeroAvailability.NotApplicable;
            }

            var hasPageHero = (currentPage as IPageHeroComposition).HasPageHeroImage();

            return hasPageHero ? PageHeroAvailability.Authored : PageHeroAvailability.NotAuthored;
        }
    }
}
