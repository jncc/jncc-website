using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.RenderMvcControllers
{
    public sealed class HomePageController : RenderMvcController
    {
        private readonly NavigationItemService _navigationItemService;
        private readonly HomePageService _homePageService;

        public HomePageController()
        {
            _navigationItemService = new NavigationItemService();
            _homePageService = new HomePageService(_navigationItemService);
        }

        public ActionResult Index(HomePage model)
        {
            var viewModel = _homePageService.GetViewModel(model);

            return CurrentTemplate(viewModel);
        }
    }
}
