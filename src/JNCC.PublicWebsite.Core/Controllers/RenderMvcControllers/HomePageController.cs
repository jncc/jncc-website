using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.RenderMvcControllers
{
    public sealed class HomePageController : RenderMvcController
    {
        private readonly NavigationItemService _navigationItemService;

        public HomePageController()
        {
            _navigationItemService = new NavigationItemService();
        }

        public ActionResult Index(HomePage model)
        {
            var homePageService = new HomePageService(_navigationItemService, Umbraco);
            var viewModel = homePageService.GetViewModel(model);

            return CurrentTemplate(viewModel);
        }
    }
}
