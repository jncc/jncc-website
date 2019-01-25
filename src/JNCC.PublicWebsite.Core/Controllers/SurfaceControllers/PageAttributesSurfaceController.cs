using System.Web.Mvc;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class PageAttributesSurfaceController : CoreSurfaceController
    {
        [ChildActionOnly]
        public ActionResult RenderAttributes()
        {
            return PartialView("~/Views/Partials/PageAttributes.cshtml", CurrentPage.GetCulture());
        }
    }
}
