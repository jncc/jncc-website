using JNCC.PublicWebsite.Core.Models;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public class FooterSurfaceController : SurfaceController
    {
        private HomePage Root { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            Root = CurrentPage.AncestorOrSelf<HomePage>();
        }

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
