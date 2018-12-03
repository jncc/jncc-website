using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class NewsAndInsightsLandingService
    {
        internal PagedResult<ArticleListingViewModel> GetViewModels(NewsAndInsightsLandingPage newsAndInsightsLandingPage, int pageNumber)
        {
            var children = newsAndInsightsLandingPage.Children<ArticlePage>();
            var results = new Paged​Result<ArticleListingViewModel>(children.LongCount(), pageNumber, newsAndInsightsLandingPage.ArticlesPerPage);
            var viewModels = children.Skip(results.GetSkipSize()).Take(newsAndInsightsLandingPage.ArticlesPerPage).Select(ToViewModel);
            results.Items = viewModels;

            return results;
        }

        private ArticleListingViewModel ToViewModel(ArticlePage content)
        {
            var viewModel = new ArticleListingViewModel
            {
                Title = string.IsNullOrWhiteSpace(content.Headline) ? content.Name : content.Headline,
                PublishDate = content.PublishDate,
                Description = content.LandingPageContent,
                Url = content.Url
            };

            if (content.HeroImage != null)
            {
                viewModel.ImageUrl = content.HeroImage.GetCropUrl(ImageCropAliases.ListingThumbnail);
            }

            return viewModel;
        }
    }
}
