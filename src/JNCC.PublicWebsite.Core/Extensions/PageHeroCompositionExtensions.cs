using JNCC.PublicWebsite.Core.Models;

namespace JNCC.PublicWebsite.Core.Extensions
{
    internal static class PageHeroCompositionExtensions
    {
        public static bool HasPageHeroImage(this IPageHeroComposition composition)
        {
            if (composition.HeroImage == null)
            {
                return false;
            }

            return string.IsNullOrEmpty(composition.HeroImage.Url) == false;
        }
    }
}
