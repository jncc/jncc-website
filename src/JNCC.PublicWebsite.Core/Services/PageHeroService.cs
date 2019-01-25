using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class PageHeroService
    {
        public PageHeroViewModel GetViewModel(IPublishedContent publishedContent)
        {
            if (publishedContent is IPageHeroComposition)
            {
                return GetPageHeroViewModel(publishedContent as IPageHeroComposition);
            }

            if (publishedContent is ArticulatePost)
            {
                return GetBlogPostViewModel(publishedContent as ArticulatePost);
            }
            else
            {
                var blogRoot = publishedContent.AncestorOrSelf<Articulate>();

                if (blogRoot != null)
                {
                    return GetBlogViewModel(blogRoot);
                }
            }

            return null;
        }

        private PageHeroViewModel GetBlogViewModel(Articulate articulate)
        {
            if (articulate.BlogBanner == null)
            {
                return null;
            }

            return new PageHeroViewModel()
            {
                Headline = articulate.Name,
                ImageUrl = articulate.GetCropUrl("blogBanner", "wide")
            };
        }

        private PageHeroViewModel GetBlogPostViewModel(ArticulatePost articulatePost)
        {
            if (articulatePost.PostImage == null)
            {
                return null;
            }

            return new PageHeroViewModel()
            {
                Headline = articulatePost.Name,
                ImageUrl = articulatePost.GetCropUrl("postImage", "wide")
            };
        }

        private PageHeroViewModel GetPageHeroViewModel(IPageHeroComposition pageHeroComposition)
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

        public NoPageHeroHeadlineViewModel GetNoPageHeroHeadlineViewModel(IPublishedContent currentPage)
        {
            if (currentPage is IPageHeroComposition)
            {
                var headline = (currentPage as IPageHeroComposition).Headline;

                if (string.IsNullOrWhiteSpace(headline) == false)
                {
                    return new NoPageHeroHeadlineViewModel()
                    {
                        Headline = headline
                    };
                }
            }

            return new NoPageHeroHeadlineViewModel()
            {
                Headline = currentPage.Name
            };
        }
    }
}
