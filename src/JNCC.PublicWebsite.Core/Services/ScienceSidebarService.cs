using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Providers;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class ScienceSidebarService
    {
        private readonly NavigationItemService _navigationItemService;
        private readonly ISciencePageCategoriesProvider _sciencePageCategoriesProvider;

        public ScienceSidebarService(NavigationItemService navigationItemService, ISciencePageCategoriesProvider sciencePageCategoriesProvider)
        {
            _navigationItemService = navigationItemService;
            _sciencePageCategoriesProvider = sciencePageCategoriesProvider;
        }

        public ScienceSidebarViewModel GetSidebarViewModel(ScienceDetailsPage model)
        {
            return new ScienceSidebarViewModel
            {
                PrimaryCallToActionButton = _navigationItemService.GetViewModel(model.SidebarPrimaryCallToActionButton),
                Categories = GetCategoriesWithFeaturedPages(model),
                SeeAlsoLinks = _navigationItemService.GetViewModels(model.SidebarSeeAlsoLinks)
            };
        }

        private IEnumerable<MainNavigationItemViewModel> GetCategoriesWithFeaturedPages(ScienceDetailsPage model)
        {
            var categories = _sciencePageCategoriesProvider.GetCategories(model);

            var viewModels = new List<MainNavigationItemViewModel>();

            foreach (var category in categories)
            {
                var viewModel = _navigationItemService.GetViewModel<MainNavigationItemViewModel>(category);
                viewModel.Children = _navigationItemService.GetViewModels<MainNavigationItemViewModel>(category.FeaturedPages);

                viewModels.Add(viewModel);
            }

            return viewModels;
        }

    }
}
