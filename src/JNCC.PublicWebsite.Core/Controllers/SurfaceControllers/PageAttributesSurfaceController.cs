using System.Web.Mvc;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class PageAttributesSurfaceController : SurfaceController
    {
        [ChildActionOnly]
        public ActionResult RenderAttributes()
        {
            return PartialView("~/Views/Partials/PageAttributes.cshtml", CurrentPage.GetCulture());
        }
    }
}
