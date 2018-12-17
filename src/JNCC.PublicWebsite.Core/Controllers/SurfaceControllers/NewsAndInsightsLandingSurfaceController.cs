using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class NewsAndInsightsLandingSurfaceController : CoreSurfaceController
    {
        [ChildActionOnly]
        public ActionResult RenderFiltering(NewsAndInsightsLandingFilteringModel model)
        {
            if (CurrentPage is NewsAndInsightsLandingPage == false)
            {
                return EmptyResult();
            }

            return PartialView("~/Views/Partials/NewsAndInsightsLanding/Filtering.cshtml");
        }

        [ChildActionOnly]
        public ActionResult RenderListing(NewsAndInsightsLandingFilteringModel model)
        {
            if (CurrentPage is NewsAndInsightsLandingPage == false)
            {
                return EmptyResult();
            }

            var service = new NewsAndInsightsLandingService();
            var results = service.GetViewModels(CurrentPage as NewsAndInsightsLandingPage, model);

            return PartialView("~/Views/Partials/NewsAndInsightsLanding/Listing.cshtml", results);
        }
    }
}
