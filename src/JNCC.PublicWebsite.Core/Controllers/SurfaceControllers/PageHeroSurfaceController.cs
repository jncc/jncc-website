using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using System.Web.Mvc;
using JNCC.PublicWebsite.Core.Extensions;

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
        [ChildActionOnly]
        public ActionResult RenderNoPageHeroHeadline()
        {
            if (CurrentPage is IPageHeroComposition == false)
            {
                return EmptyResult();
            }

            var composition = CurrentPage as IPageHeroComposition;

            if (composition.HasPageHeroImage())
            {
                return EmptyResult();
            }

            return PartialView("~/Views/Partials/NoPageHeroHeadline.cshtml", composition);
        }

    }
}
