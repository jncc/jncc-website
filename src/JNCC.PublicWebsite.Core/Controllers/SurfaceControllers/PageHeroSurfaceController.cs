using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class PageHeroSurfaceController : CoreSurfaceController
    {
        private readonly PageHeroService _pageHeroService = new PageHeroService();

        [ChildActionOnly]
        public ActionResult RenderPageHero()
        {
            var viewModel = _pageHeroService.GetViewModel(CurrentPage);

            if (viewModel == null)
            {
                return EmptyResult();
            }

            return PartialView("~/Views/Partials/PageHero.cshtml", viewModel);
        }

        [ChildActionOnly]
        public ActionResult RenderNoPageHeroHeadline()
        {
            if (_pageHeroService.HasPageHero(CurrentPage))
            {
                return EmptyResult();
            }

            var viewModel = _pageHeroService.GetNoPageHeroHeadlineViewModel(CurrentPage);

            if (viewModel == null)
            {
                return EmptyResult();
            }

            return PartialView("~/Views/Partials/NoPageHeroHeadline.cshtml", viewModel);
        }

    }
}
