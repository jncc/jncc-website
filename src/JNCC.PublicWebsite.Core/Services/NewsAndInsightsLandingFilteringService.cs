using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Providers;
using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Core.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class NewsAndInsightsLandingFilteringService : FilteringService<NewsAndInsightsLandingFilteringModel, NewsAndInsightsLandingFilteringViewModel, IPublishedContent>
    {
        private readonly IArticleTypesProvider<IPublishedContent> _articleTypesProvider;
        private readonly IArticleYearsProvider<IPublishedContent> _articleYearsProvider;

        public NewsAndInsightsLandingFilteringService(ITagsProvider tagsProvider, IArticleTypesProvider<IPublishedContent> articleTypesProvider, IArticleYearsProvider<IPublishedContent> articleYearsProvider) : base(tagsProvider)
        {
            _articleTypesProvider = articleTypesProvider;
            _articleYearsProvider = articleYearsProvider;
        }

        public override NewsAndInsightsLandingFilteringViewModel GetFilteringViewModel(NewsAndInsightsLandingFilteringModel filteringModel, IPublishedContent root)
        {
            var allTeams = GetAllTeams();
            var articleTypes = _articleTypesProvider.GetAllByRoot(root);
            var articleYears = _articleYearsProvider.GetAllByRootDescending(root);

            var viewModel = new NewsAndInsightsLandingFilteringViewModel()
            {
                Teams = new FilterGroupViewModel()
                {
                    Title = "Team",
                    Group = FilterNames.Teams,
                    Values = GetFilters(allTeams, filteringModel.Teams)
                },
                ArticleTypes = new FilterGroupViewModel()
                {
                    Title = "Article Type",
                    Group = FilterNames.ArticleTypes,
                    Values = GetFilters(articleTypes, filteringModel.ArticleTypes)
                },
                Years = new FilterGroupViewModel()
                {
                    Title = "Year",
                    Group = FilterNames.Years,
                    Values = GetFilters(articleYears, filteringModel.Years)
                }
            };

            if (string.IsNullOrWhiteSpace(filteringModel.SearchTerm) == false)
            {
                viewModel.SearchTerm = filteringModel.SearchTerm;
            }

            return viewModel;
        }
    }
}
