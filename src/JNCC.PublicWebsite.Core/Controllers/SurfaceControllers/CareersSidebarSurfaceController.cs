using JNCC.PublicWebsite.Core.Configuration;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class CareersSidebarSurfaceController : CoreSurfaceController
    {
        private readonly NavigationItemService _navigationItemService;
        private readonly CareersSidebarService _careersSidebarService;
        private readonly IDataHubRawQueryService _dataHubRawQueryService;

        public CareersSidebarSurfaceController()
        {
            var config = SearchConfiguration.GetConfig();
            _dataHubRawQueryService = new SearchService(config);
            _navigationItemService = new NavigationItemService();
            _careersSidebarService = new CareersSidebarService(_navigationItemService, _dataHubRawQueryService);
        }

        [ChildActionOnly]
        public ActionResult RenderSidebar()
        {
            if (CurrentPage is CareersLandingPage == false)
            {
                return EmptyResult();
            }

            var viewModel = _careersSidebarService.GetViewModel(CurrentPage as CareersLandingPage);
            return PartialView("~/Views/Partials/CareersSidebar.cshtml", viewModel);
        }
    }
}
