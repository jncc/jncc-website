using JNCC.PublicWebsite.Core.ViewModels;
using JNCC.PublicWebsite.Core.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    internal abstract class SidebarServiceBase
    {
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
                SeeAlsoLinks = _navigationItemService.GetViewModels(composition.SidebarSeeAlsoLinks)
            };
        }
    }
}