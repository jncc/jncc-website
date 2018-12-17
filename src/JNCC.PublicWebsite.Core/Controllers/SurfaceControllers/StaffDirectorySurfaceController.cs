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
        public ActionResult RenderFiltering(string[] locations, string[] teams, string searchTerm)
        {
            if (CurrentPage is StaffDirectoryPage == false)
            {
                return EmptyResult();
            }

            var filters = new StaffDirectoryFilteringModel
            {
                Locations = locations,
                Teams = teams,
                SearchTerm = searchTerm
            };

            var service = new StaffDirectoryFilteringService(Services.TagService);
            var viewModel = service.GetFilteringViewModel(filters);

            return PartialView("~/Views/Partials/StaffDirectory/Filtering.cshtml", viewModel);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult RenderListing(string[] locations, string[] teams, string searchTerm, int pageNumber = 1)
        {
            if (CurrentPage is StaffDirectoryPage == false)
            {
                return EmptyResult();
            }

            var filters = new StaffDirectoryFilteringModel
            {
                Locations = locations,
                Teams = teams,
                PageNumber = pageNumber,
                SearchTerm = searchTerm
            };

            var viewModel = new StaffDirectoryListingViewModel
            {
                Items = _staffDirectoryService.GetViewModels(CurrentPage as StaffDirectoryPage, filters),
                Filters = _staffDirectoryService.ConvertFiltersToNameValueCollection(filters)
            };

            return PartialView("~/Views/Partials/StaffDirectory/Listing.cshtml", viewModel);
        }
    }
}
