using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Providers;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using Umbraco.Core;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class ScienceSidebarService : SidebarServiceBase
    {
        private readonly ISciencePageCategoriesProvider _sciencePageCategoriesProvider;

        public ScienceSidebarService(NavigationItemService navigationItemService, IDataHubRawQueryService dataHubRawQueryService, ISciencePageCategoriesProvider sciencePageCategoriesProvider) : base(navigationItemService, dataHubRawQueryService)
        {
            _sciencePageCategoriesProvider = sciencePageCategoriesProvider;
        }

        public ScienceSidebarViewModel GetSidebarViewModel(ScienceDetailsPage model)
        {
            var viewModel = CreateViewModel<ScienceSidebarViewModel>(model);
            viewModel.Categories = GetCategoriesWithFeaturedPages(model);

            return viewModel;
        }

        public ScienceSidebarViewModel GetSidebarViewModel(ScienceCategoryPage model)
        {
            var viewModel = CreateViewModel<ScienceSidebarViewModel>(model);
            viewModel.Categories = GetCategoriesWithFeaturedPages(model);

            return viewModel;
        }

        private IEnumerable<MainNavigationItemViewModel> GetCategoriesWithFeaturedPages(ScienceDetailsPage model)
        {
            var categories = _sciencePageCategoriesProvider.GetCategories(model);

            return GetCategoriesWithFeaturedPages(categories);
        }

        private IEnumerable<MainNavigationItemViewModel> GetCategoriesWithFeaturedPages(ScienceCategoryPage model)
        {
            var category = GetCategoryWithFeaturedPages(model);

            return category.AsEnumerableOfOne();
        }

        private IEnumerable<MainNavigationItemViewModel> GetCategoriesWithFeaturedPages(IEnumerable<ScienceCategoryPage> categories)
        {
            if (ExistenceUtility.IsNullOrEmpty(categories))
            {
                return null;
            }

            var viewModels = new List<MainNavigationItemViewModel>();

            foreach (var category in categories)
            {
                var viewModel = GetCategoryWithFeaturedPages(category);
                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        private MainNavigationItemViewModel GetCategoryWithFeaturedPages(ScienceCategoryPage category)
        {
            var viewModel = _navigationItemService.GetViewModel<MainNavigationItemViewModel>(category);
            viewModel.Children = _navigationItemService.GetViewModels<MainNavigationItemViewModel>(category.FeaturedPages);

            return viewModel;
        }
    }
}
