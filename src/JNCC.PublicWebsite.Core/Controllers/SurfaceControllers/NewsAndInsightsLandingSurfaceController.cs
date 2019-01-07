using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Providers;
using JNCC.PublicWebsite.Core.Services;
using JNCC.PublicWebsite.Core.ViewModels;
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
            var tagsProvider = new UmbracoContentTagsProvider(Services.TagService);
            var articleTypesProvider = new UmbracoArticleTypesProvider();
            var articleYearsProvider = new UmbracoArticleYearsProvider();

            var service = new NewsAndInsightsLandingFilteringService(tagsProvider, articleTypesProvider, articleYearsProvider);
            var viewModel = service.GetFilteringViewModel(model, CurrentPage);

            return PartialView("~/Views/Partials/NewsAndInsightsLanding/Filtering.cshtml", viewModel);
        }

        [ChildActionOnly]
        public ActionResult RenderListing(NewsAndInsightsLandingFilteringModel model)
        {
            if (CurrentPage is NewsAndInsightsLandingPage == false)
            {
                return EmptyResult();
            }

            var service = new NewsAndInsightsLandingService();

            var viewModel = new NewsAndInsightsLandingListingViewModel
            {
                Items = service.GetViewModels(CurrentPage as NewsAndInsightsLandingPage, model),
                Filters = service.ConvertFiltersToNameValueCollection(model)
            };

            return PartialView("~/Views/Partials/NewsAndInsightsLanding/Listing.cshtml", viewModel);
        }
    }
}
