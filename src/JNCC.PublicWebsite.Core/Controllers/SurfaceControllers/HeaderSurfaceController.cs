using JNCC.PublicWebsite.Core.Services;
using System.Linq;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class HeaderSurfaceController : CoreSurfaceController
    {
        private readonly MainNavigationService mainNavigationService = new MainNavigationService();

        [ChildActionOnly]
        public ActionResult RenderMainNavigation()
        {
            var menuItems = mainNavigationService.GetRootMenuItems(Root);

            if (menuItems == null || menuItems.Any() == false)
            {
                return EmptyResult();
            }

            return PartialView("~/Views/Partials/Header/MainNavigation.cshtml", menuItems);
        }

        [ChildActionOnly]
        public PartialViewResult RenderSearch()
        {
            return PartialView("~/Views/Partials/Header/Search.cshtml");
        }
    }
}
