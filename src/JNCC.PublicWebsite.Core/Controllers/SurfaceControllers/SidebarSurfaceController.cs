using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class SidebarSurfaceController : CoreSurfaceController
    {
        [HttpGet]
        public ActionResult RenderSidebar()
        {
            return PartialView("~/Views/Partials/Sidebar.cshtml");
        }
    }
}
