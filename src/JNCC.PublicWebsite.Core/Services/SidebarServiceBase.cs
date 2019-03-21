using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.Services
{
    internal abstract class SidebarServiceBase
    {
        private const int _maxNumberOfResults = 5;
        protected readonly NavigationItemService _navigationItemService;
        protected readonly IDataHubRawQueryService _dataHubRawQueryService;

        public SidebarServiceBase(NavigationItemService navigationItemService, IDataHubRawQueryService dataHubRawQueryService)
        {
            _navigationItemService = navigationItemService;
            _dataHubRawQueryService = dataHubRawQueryService;
        }

        protected T CreateViewModel<T>(ISidebarComposition composition) where T : BasicSidebarViewModel, new()
        {
            return new T
            {
                PrimaryCallToActionButton = _navigationItemService.GetViewModel(composition.SidebarPrimaryCallToActionButton),
                DataHubLinks = GetDataHubLinks(composition),
                SeeAlsoLinks = _navigationItemService.GetViewModels(composition.SidebarSeeAlsoLinks)
            };
        }

        private IEnumerable<NavigationItemViewModel> GetDataHubLinks(ISidebarComposition composition)
        {
            var viewModels = new List<NavigationItemViewModel>();
            if (string.IsNullOrWhiteSpace(composition.SidebarDataHubQuery))
            {
                return viewModels;
            }

            var results = _dataHubRawQueryService.GetByRawQuery(composition.SidebarDataHubQuery, _maxNumberOfResults);
            if (ExistenceUtility.IsNullOrEmpty(results.Hits.Results))
            {
                return viewModels;
            }

            foreach (var result in results.Hits.Results)
            {
                var viewModel = new NavigationItemViewModel()
                {
                    Text = result.Source.Title,
                    Url = result.Source.Url,
                    Target = HtmlAnchorTargets.Blank
                };

                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}