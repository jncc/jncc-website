using JNCC.PublicWebsite.Core.Services;
using JNCC.PublicWebsite.Core.Utilities;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class HeaderSurfaceController : CoreSurfaceController
    {
        private readonly MainNavigationService mainNavigationService = new MainNavigationService();

        [ChildActionOnly]
        public ActionResult RenderMainNavigation()
        {
            var menuItems = mainNavigationService.GetRootMenuItems(Root, CurrentPage);

            if (ExistenceUtility.IsNullOrEmpty(menuItems))
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
