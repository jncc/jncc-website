using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class StaffDirectorySurfaceController : CoreSurfaceController
    {
        private readonly StaffDirectoryService _staffDirectoryService = new StaffDirectoryService();

        [ChildActionOnly]
        public ActionResult RenderFiltering()
        {
            if (CurrentPage is StaffDirectoryPage == false)
            {
                return EmptyResult();
            }

            return PartialView("~/Views/Partials/StaffDirectory/Filtering.cshtml");
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
