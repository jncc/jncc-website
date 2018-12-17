using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class StaffDirectorySurfaceController : CoreSurfaceController
    {
        private readonly StaffDirectoryService _staffDirectoryService = new StaffDirectoryService();

        [ChildActionOnly]
        public ActionResult RenderFiltering(StaffDirectoryFilteringModel model)
        {
            if (CurrentPage is StaffDirectoryPage == false)
            {
                return EmptyResult();
            }

            var service = new StaffDirectoryFilteringService(Services.TagService);
            var viewModel = service.GetFilteringViewModel(model);

            return PartialView("~/Views/Partials/StaffDirectory/Filtering.cshtml", viewModel);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult RenderListing(StaffDirectoryFilteringModel model)
        {
            if (CurrentPage is StaffDirectoryPage == false)
            {
                return EmptyResult();
            }

            var viewModel = new StaffDirectoryListingViewModel
            {
                Items = _staffDirectoryService.GetViewModels(CurrentPage as StaffDirectoryPage, model),
                Filters = _staffDirectoryService.ConvertFiltersToNameValueCollection(model)
            };

            return PartialView("~/Views/Partials/StaffDirectory/Listing.cshtml", viewModel);
        }
    }
}
