using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Specialized;
using System.Linq;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class NewsAndInsightsLandingService : ListingService<NewsAndInsightsLandingPage, ArticlePage, ArticleListingViewModel, FilteringModel>
    {
        public override NameValueCollection ConvertFiltersToNameValueCollection(FilteringModel filteringModel)
        {
            return new NameValueCollection();
        }

        protected override int GetItemsPerPage(NewsAndInsightsLandingPage parent)
        {
            return parent.ArticlesPerPage;
        }

        protected override IOrderedEnumerable<ArticlePage> GetOrderedChildren(NewsAndInsightsLandingPage parent, FilteringModel filteringModel)
        {
            return parent.Children<ArticlePage>()
                         .OrderByDescending(x => x.PublishDate)
                         .ThenByFirstAvailableProperty(x => new string[] { x.Headline, x.Name });
        }

        protected override ArticleListingViewModel ToViewModel(ArticlePage content)
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
