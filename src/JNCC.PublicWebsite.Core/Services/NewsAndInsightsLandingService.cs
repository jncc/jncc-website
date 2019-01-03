using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Umbraco.Core;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class NewsAndInsightsLandingService : ListingService<NewsAndInsightsLandingPage, ArticlePage, ArticleListingViewModel, NewsAndInsightsLandingFilteringModel>
    {
        public override NameValueCollection ConvertFiltersToNameValueCollection(NewsAndInsightsLandingFilteringModel filteringModel)
        {
            return new NameValueCollection();
        }

        protected override int GetItemsPerPage(NewsAndInsightsLandingPage parent)
        {
            return parent.ArticlesPerPage;
        }

        protected override IOrderedEnumerable<ArticlePage> GetOrderedChildren(NewsAndInsightsLandingPage parent, NewsAndInsightsLandingFilteringModel filteringModel)
        {
            var allChildren = parent.Children<ArticlePage>();
            var conditions = new List<Func<ArticlePage, bool>>();

            if (ExistenceUtility.IsNullOrEmpty(filteringModel.Years) == false)
            {
                conditions.Add(x => filteringModel.Years.Contains(x.PublishDate.Year));
            }

            if (ExistenceUtility.IsNullOrEmpty(filteringModel.ArticleTypes) == false)
            {
                conditions.Add(x => filteringModel.ArticleTypes.Contains(x.ArticleType));
            }

            if (string.IsNullOrEmpty(filteringModel.SearchTerm) == false)
            {
                conditions.Add(x => x.Name.InvariantContains(filteringModel.SearchTerm)
                                 || x.Headline.InvariantContains(filteringModel.SearchTerm) 
                                 || (ExistenceUtility.IsNullOrWhiteSpace(x.LandingPageContent) == false && x.LandingPageContent.ToString().InvariantContains(filteringModel.SearchTerm))
                                 || (ExistenceUtility.IsNullOrWhiteSpace(x.MainContent) == false && x.MainContent.ToString().InvariantContains(filteringModel.SearchTerm))
                              );
            }

            var actualChildren = ExistenceUtility.IsNullOrEmpty(conditions) ? allChildren : allChildren.Where(x => conditions.All(y => y.Invoke(x)));

            return actualChildren.OrderByDescending(x => x.PublishDate)
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
