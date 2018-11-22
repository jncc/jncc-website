using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public class HeaderSurfaceController : SurfaceController
    {
        [ChildActionOnly]
        public PartialViewResult RenderMainNavigation()
        {
            return PartialView("~/Views/Partials/Header/MainNavigation.cshtml");
        }
    }
}
