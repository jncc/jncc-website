using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class StaffDirectorySurfaceController : CoreSurfaceController
    {
        private readonly StaffDirectoryService _staffDirectoryService = new StaffDirectoryService();

        [ChildActionOnly]
        public ActionResult RenderFiltering(string[] locations, string[] teams)
        {
            if (CurrentPage is StaffDirectoryPage == false)
            {
                return EmptyResult();
            }
            var service = new StaffDirectoryFilteringService(Services.TagService);
            var viewModel = service.GetFilteringViewModel(locations, teams);

            return PartialView("~/Views/Partials/StaffDirectory/Filtering.cshtml", viewModel);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult RenderListing(int pageNumber = 1)
        {
            if (CurrentPage is StaffDirectoryPage == false)
            {
                return EmptyResult();
            }

            var results = _staffDirectoryService.GetViewModels(CurrentPage as StaffDirectoryPage, pageNumber);

            return PartialView("~/Views/Partials/StaffDirectory/Listing.cshtml", results);
        }
    }
}
