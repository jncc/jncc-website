using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Providers;
using JNCC.PublicWebsite.Core.Services;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class StaffDirectorySurfaceController : CoreSurfaceController
    {
        [ChildActionOnly]
        public ActionResult RenderFiltering(StaffDirectoryFilteringModel model)
        {
            if (CurrentPage is StaffDirectoryPage == false)
            {
                return EmptyResult();
            }
            var tagsProvider = new UmbracoContentTagsProvider(Services.TagService);
            var service = new StaffDirectoryFilteringService(tagsProvider);
            var viewModel = service.GetFilteringViewModel(model, CurrentPage);

            return PartialView("~/Views/Partials/StaffDirectory/Filtering.cshtml", viewModel);
        }

        [ChildActionOnly]
        public ActionResult RenderListing(StaffDirectoryFilteringModel model)
        {
            if (CurrentPage is StaffDirectoryPage == false)
            {
                return EmptyResult();
            }

            var service = new StaffDirectoryService();
            var viewModel = new StaffDirectoryListingViewModel
            {
                Items = service.GetViewModels(CurrentPage as StaffDirectoryPage, model),
                Filters = service.ConvertFiltersToNameValueCollection(model)
            };

            return PartialView("~/Views/Partials/StaffDirectory/Listing.cshtml", viewModel);
        }
    }
}
