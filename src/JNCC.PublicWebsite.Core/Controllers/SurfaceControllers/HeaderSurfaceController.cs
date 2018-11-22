using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class HeaderSurfaceController : CoreSurfaceController
    {
        [ChildActionOnly]
        public PartialViewResult RenderMainNavigation()
        {
            return PartialView("~/Views/Partials/Header/MainNavigation.cshtml");
        }

        [ChildActionOnly]
        public PartialViewResult RenderSearch()
        {
            return PartialView("~/Views/Partials/Header/Search.cshtml");
        }
    }
}
