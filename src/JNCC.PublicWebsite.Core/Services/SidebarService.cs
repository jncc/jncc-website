using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class SidebarService
    {
        private readonly NavigationItemService _navigationItemService;
        private const int _sectionRootLevel = 2;

        public SidebarService(NavigationItemService navigationItemService)
        {
            _navigationItemService = navigationItemService;
        }

        public SidebarViewModel GetViewModel(ISidebarComposition composition)
        {
            var viewModel = new SidebarViewModel()
            {
                GetInTouchButton = _navigationItemService.GetViewModel(composition.SidebarGetInTouchButton),
                InThisSectionLinks = GetInThisSectionLinks(composition),
                SeeAlsoLinks = _navigationItemService.GetViewModels(composition.SidebarSeeAlsoLinks)
            };

            return viewModel;
        }

        private IEnumerable<NavigationItemViewModel> GetInThisSectionLinks(ISidebarComposition composition)
        {
            var sectionRoot = composition.AncestorOrSelf(_sectionRootLevel);

            if (sectionRoot == null)
            {
                return null;
            }

            return _navigationItemService.GetViewModels(sectionRoot.Children);
        }
    }
}
