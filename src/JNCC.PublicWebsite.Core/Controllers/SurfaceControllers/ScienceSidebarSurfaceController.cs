using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Providers;
using JNCC.PublicWebsite.Core.Services;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class ScienceSidebarSurfaceController : CoreSurfaceController
    {
        private readonly NavigationItemService _navigationItemService;
        private readonly ScienceSidebarService _sidebarService;

        public ScienceSidebarSurfaceController()
        {
            var categoriesProvider = new UmbracoSciencePageCategoriesProvider(ApplicationContext.ApplicationCache.RequestCache);

            _navigationItemService = new NavigationItemService();
            _sidebarService = new ScienceSidebarService(_navigationItemService, categoriesProvider);
        }

        [ChildActionOnly]
        public ActionResult RenderScienceDetailsPageSidebar()
        {
            if (CurrentPage is ScienceDetailsPage == false)
            {
                return EmptyResult();
            }

            var viewModel = _sidebarService.GetSidebarViewModel(CurrentPage as ScienceDetailsPage);

            return PartialView("~/Views/Partials/ScienceSidebar.cshtml", viewModel);
        }
    }
}
