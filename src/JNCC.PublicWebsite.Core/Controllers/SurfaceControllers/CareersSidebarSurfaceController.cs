using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class CareersSidebarSurfaceController : CoreSurfaceController
    {
        private readonly NavigationItemService _navigationItemService;
        private readonly CareersLandingPageService _careersLandingPageService;

        public CareersSidebarSurfaceController()
        {
            _navigationItemService = new NavigationItemService();
            _careersLandingPageService = new CareersLandingPageService(_navigationItemService);
        }

        [ChildActionOnly]
        public ActionResult RenderSidebar()
        {
            if (CurrentPage is CareersLandingPage == false)
            {
                return EmptyResult();
            }

            var viewModel = _careersLandingPageService.GetSidebarViewModel(CurrentPage as CareersLandingPage);
            return PartialView("~/Views/Partials/CareersSidebar.cshtml", viewModel);
        }
    }
}
