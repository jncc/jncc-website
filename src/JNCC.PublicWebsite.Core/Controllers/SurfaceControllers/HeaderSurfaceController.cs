using JNCC.PublicWebsite.Core.Services;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class HeaderSurfaceController : CoreSurfaceController
    {
        private readonly MainNavigationService mainNavigationService = new MainNavigationService();
        private readonly PageHeroService _pageHeroService = new PageHeroService();
        private readonly MemberLoggedInBarService _memberLoggedInBarService = new MemberLoggedInBarService();

        [ChildActionOnly]
        public ActionResult RenderMemberLoggedInBar()
        {
            if (Members.IsLoggedIn() == false)
            {
                return EmptyResult();
            }

            var viewModel = _memberLoggedInBarService.GetViewModel(Root);

            return View("~/Views/Partials/Header/MemberLoggedInBar.cshtml", viewModel);
        }

        [ChildActionOnly]
        public ActionResult RenderMainNavigation()
        {
            var viewModel = new MainNavigationViewModel
            {
                Items = mainNavigationService.GetRootMenuItems(Root, CurrentPage),
                HasPageHero = _pageHeroService.HasPageHero(CurrentPage)
            };

            if (ExistenceUtility.IsNullOrEmpty(viewModel.Items))
            {
                return EmptyResult();
            }

            return PartialView("~/Views/Partials/Header/MainNavigation.cshtml", viewModel);
        }

        [ChildActionOnly]
        public PartialViewResult RenderSearch()
        {
            return PartialView("~/Views/Partials/Header/Search.cshtml");
        }
    }
}
