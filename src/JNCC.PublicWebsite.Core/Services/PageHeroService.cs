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
            var isPageHeroComposition = currentPage is IPageHeroComposition;
            var isPageHeroCarouselComposition = currentPage is IPageHeroCarouselComposition;
            var isArticulatePost = currentPage is ArticulatePost;
            var blogRoot = currentPage.AncestorOrSelf<Articulate>();
            var hasBlogRoot = blogRoot != null;

            if (isPageHeroComposition == false && isPageHeroCarouselComposition == false && isArticulatePost == false && hasBlogRoot == false)
            {
                return false;
            }

            if (isPageHeroComposition)
            {
                return (currentPage as IPageHeroComposition).HasPageHeroImage();
            }

            if (isArticulatePost)
            {
                var post = (currentPage as ArticulatePost);
                var postImage = post.PostImage;

                if (postImage == null)
                {
                    return false;
                }

                return postImage.HasCrop("wide");
            }
            else if (hasBlogRoot)
            {
                var blogBanner = blogRoot.BlogBanner;

                if (blogBanner == null)
                {
                    return false;
                }

                return blogBanner.HasCrop("wide");
            }

            if (isPageHeroCarouselComposition)
            {
                return ExistenceUtility.IsNullOrEmpty((currentPage as IPageHeroCarouselComposition).HeroImages) == false;
            }

            return false;
        }
    }
}
