using JNCC.PublicWebsite.Core.Models;
using System.Web.Mvc;
using System.Web.Routing;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class StaffDirectorySurfaceController : CoreSurfaceController
    {
        [ChildActionOnly]
        public ActionResult RenderFiltering()
        {
            if (CurrentPage is StaffDirectoryPage == false)
            {
                return EmptyResult();
            }

            return PartialView("~/Views/Partials/StaffDirectory/Filtering.cshtml");
        }

        [ChildActionOnly]
        public ActionResult RenderListing()
        {
            if (CurrentPage is StaffDirectoryPage == false)
            {
                return EmptyResult();
            }

            return PartialView("~/Views/Partials/StaffDirectory/Listing.cshtml");
        }
    }
}
