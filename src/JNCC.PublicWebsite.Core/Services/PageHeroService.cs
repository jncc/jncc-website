using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;

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
    }
}
