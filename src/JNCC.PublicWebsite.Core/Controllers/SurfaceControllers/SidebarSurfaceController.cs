using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class SidebarSurfaceController : CoreSurfaceController
    {
        private readonly NavigationItemService _navigationItemService;
        private readonly SidebarService _sidebarService;

        public SidebarSurfaceController()
        {
            _navigationItemService = new NavigationItemService();
            _sidebarService = new SidebarService(_navigationItemService);
        }

        [HttpGet]
        public ActionResult RenderSidebar()
        {
            if (CurrentPage is ISidebarComposition == false)
            {
                return EmptyResult();
            }

            var viewModel = _sidebarService.GetViewModel(CurrentPage as ISidebarComposition);

            return PartialView("~/Views/Partials/Sidebar.cshtml", viewModel);
        }
    }
}
