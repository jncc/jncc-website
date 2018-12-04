using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class CookieBannerTemplateSurfaceController : CoreSurfaceController
    {
        [ChildActionOnly]
        public ActionResult RenderCookieBannerTemplate()
        {
            if (Root == null || Root.CookieBannerContent == null)
            {
                return EmptyResult();
            }

            return PartialView("~/Views/Partials/CookieBannerTemplate.cshtml", Root.CookieBannerContent);
        }
    }
}
