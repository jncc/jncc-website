using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class RelatedItemsSurfaceController : CoreSurfaceController
    {
        [ChildActionOnly]
        public ActionResult RenderRelatedItems()
        {
            return PartialView("~/Views/Partials/RelatedItems.cshtml");
        }
    }
}
