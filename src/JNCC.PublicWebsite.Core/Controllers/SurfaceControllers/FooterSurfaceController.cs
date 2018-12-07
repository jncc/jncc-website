using JNCC.PublicWebsite.Core.Services;
using JNCC.PublicWebsite.Core.Utilities;
using System.Web;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class FooterSurfaceController : CoreSurfaceController
    {
        private readonly NavigationItemService _navigationItemService;
        private readonly SocialMediaLinksService _socialMediaLinksService;
        private readonly CategorisedFooterLinksService _categorisedFooterLinksService;

        public FooterSurfaceController()
        {
            _navigationItemService = new NavigationItemService();
            _socialMediaLinksService = new SocialMediaLinksService(_navigationItemService);
            _categorisedFooterLinksService = new CategorisedFooterLinksService(_navigationItemService);
        }

        [ChildActionOnly]
        public ActionResult RenderCategorisedLinks()
        {
            var categorisedLinks = _categorisedFooterLinksService.GetViewModels(Root.FooterCategorisedLinks);

            if (ExistenceUtility.IsNullOrEmpty(categorisedLinks))
            {
                return EmptyResult();
            }

            return PartialView("~/Views/Partials/Footer/CategorisedLinks.cshtml", categorisedLinks);
        }

        [ChildActionOnly]
        public ActionResult RenderSocialMediaLinks()
        {
            var links = _socialMediaLinksService.GetSocialMediaLinks(Root.FooterSocialMediaLinks);

            if (ExistenceUtility.IsNullOrEmpty(links))
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
            var links = _navigationItemService.GetViewModels(Root.FooterUncategorisedLinks);

            if (ExistenceUtility.IsNullOrEmpty(links))
            {
                return EmptyResult();
            }

            return PartialView("~/Views/Partials/Footer/UncategorisedLinks.cshtml", links);
        }
    }
}
