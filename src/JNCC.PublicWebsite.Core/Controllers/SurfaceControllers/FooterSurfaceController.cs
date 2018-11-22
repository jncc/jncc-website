using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public class FooterSurfaceController : SurfaceController
    {
        [ChildActionOnly]
        public PartialViewResult RenderCategorisedLinks()
        {
            return PartialView("~/Views/Partials/Footer/CategorisedLinks.cshtml");
        }

        [ChildActionOnly]
        public PartialViewResult RenderSocialMediaLinks()
        {
            return PartialView("~/Views/Partials/Footer/SocialMediaLinks.cshtml");
        }
    }
}
