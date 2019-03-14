using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.RenderMvcControllers
{
    public sealed class CareersLandingPageController : RenderMvcController
    {
        private readonly NavigationItemService _navigationItemService;
        private readonly CareersLandingPageService _careersLandingPageService;

        public CareersLandingPageController()
        {
            _navigationItemService = new NavigationItemService();
            _careersLandingPageService = new CareersLandingPageService(_navigationItemService);
        }

        public ActionResult Index(CareersLandingPage model)
        {
            var viewModel = _careersLandingPageService.GetViewModel(model);

            return CurrentTemplate(viewModel);
        }
    }
}
