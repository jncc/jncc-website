using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class NewsAndInsightsLandingSurfaceController : CoreSurfaceController
    {
        private readonly NewsAndInsightsLandingService _newsAndInsightsLandingService = new NewsAndInsightsLandingService();

        [ChildActionOnly]
        public ActionResult RenderFiltering()
        {
            if (CurrentPage is NewsAndInsightsLandingPage == false)
            {
                return EmptyResult();
            }

            return PartialView("~/Views/Partials/NewsAndInsightsLanding/Filtering.cshtml");
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult RenderListing(int pageNumber = 1)
        {
            if (CurrentPage is NewsAndInsightsLandingPage == false)
            {
                return EmptyResult();
            }

            var filtering = new FilteringModel()
            {
                PageNumber = pageNumber
            };

            var results = _newsAndInsightsLandingService.GetViewModels(CurrentPage as NewsAndInsightsLandingPage, filtering);

            return PartialView("~/Views/Partials/NewsAndInsightsLanding/Listing.cshtml", results);
        }
    }
}
