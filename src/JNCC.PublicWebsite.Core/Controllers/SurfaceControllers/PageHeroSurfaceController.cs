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
            if (CurrentPage is IPageHeroComposition == false)
            {
                return EmptyResult();
            }

            var viewModel = _pageHeroService.GetViewModel(CurrentPage as IPageHeroComposition);

            if (viewModel == null)
            {
                return EmptyResult();
            }

            return PartialView("~/Views/Partials/PageHero.cshtml", viewModel);
        }
    }
}
