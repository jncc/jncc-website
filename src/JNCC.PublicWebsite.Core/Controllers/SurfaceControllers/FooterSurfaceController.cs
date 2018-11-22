using System.Web;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class FooterSurfaceController : CoreSurfaceController
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

        [ChildActionOnly]
        public IHtmlString RenderContent()
        {
            return Root.FooterContent;
        }

        [ChildActionOnly]
        public PartialViewResult RenderUncategorisedLinks()
        {
            return PartialView("~/Views/Partials/Footer/UncategorisedLinks.cshtml");
        }
    }
}
