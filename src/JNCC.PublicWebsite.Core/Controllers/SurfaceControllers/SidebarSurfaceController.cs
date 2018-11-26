using JNCC.PublicWebsite.Core.Models;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class SidebarSurfaceController : CoreSurfaceController
    {
        [HttpGet]
        public ActionResult RenderSidebar()
        {
            if (CurrentPage is ISidebarComposition == false)
            {
                return EmptyResult();
            }

            return PartialView("~/Views/Partials/Sidebar.cshtml");
        }
    }
}
