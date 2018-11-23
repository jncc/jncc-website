using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class PageHeroSurfaceController : CoreSurfaceController
    {
        [ChildActionOnly]
        public ActionResult RenderPageHero()
        {
            return PartialView("~/Views/Partials/PageHero.cshtml");
        }
    }
}
