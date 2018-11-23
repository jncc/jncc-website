using JNCC.PublicWebsite.Core.Services;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class FooterSurfaceController : CoreSurfaceController
    {
        private readonly NavigationItemService navigationItemService = new NavigationItemService();
        private readonly SocialMediaLinksService socialMediaLinksService = new SocialMediaLinksService();
        private readonly CategorisedFooterLinksService categorisedFooterLinksService = new CategorisedFooterLinksService();

        [ChildActionOnly]
        public ActionResult RenderCategorisedLinks()
        {
            var categorisedLinks = categorisedFooterLinksService.GetViewModels(Root.FooterCategorisedLinks);

            if (categorisedLinks == null || categorisedLinks.Any() == false)
            {
                return EmptyResult();
            }

            return PartialView("~/Views/Partials/Footer/CategorisedLinks.cshtml", categorisedLinks);
        }

        [ChildActionOnly]
        public ActionResult RenderSocialMediaLinks()
        {
            var links = socialMediaLinksService.GetSocialMediaLinks(Root.FooterSocialMediaLinks);

            if (links == null || links.Any() == false)
            {
                return EmptyResult();
            }

            return PartialView("~/Views/Partials/Footer/SocialMediaLinks.cshtml", links);
        }

        [ChildActionOnly]
        public IHtmlString RenderContent()
        {
            return Root.FooterContent;
        }

        [ChildActionOnly]
        public ActionResult RenderUncategorisedLinks()
        {
            var links = navigationItemService.GetViewModels(Root.FooterUncategorisedLinks);

            if (links == null || links.Any() == false)
            {
                return EmptyResult();
            }

            return PartialView("~/Views/Partials/Footer/UncategorisedLinks.cshtml", links);
        }
    }
}
