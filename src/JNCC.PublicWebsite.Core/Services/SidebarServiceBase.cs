using JNCC.PublicWebsite.Core.ViewModels;
using JNCC.PublicWebsite.Core.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    internal abstract class SidebarServiceBase
    {
        protected readonly NavigationItemService _navigationItemService;

        public SidebarServiceBase(NavigationItemService navigationItemService)
        {
            _navigationItemService = navigationItemService;
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