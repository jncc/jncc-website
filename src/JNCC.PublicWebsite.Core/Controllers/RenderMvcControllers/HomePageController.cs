using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.RenderMvcControllers
{
    public sealed class HomePageController : RenderMvcController
    {
        private readonly NavigationItemService _navigationItemService;
        private readonly CalloutCardsService _calloutCardsService;

        public HomePageController()
        {
            _navigationItemService = new NavigationItemService();
            _calloutCardsService = new CalloutCardsService(_navigationItemService);
        }

        public ActionResult Index(HomePage model)
        {
            var latestNewsSectionService = new LatestNewsSectionService(Umbraco);
            var homePageService = new HomePageService(_calloutCardsService, _navigationItemService, latestNewsSectionService);
            var viewModel = homePageService.GetViewModel(model);

            return CurrentTemplate(viewModel);
        }
    }
}
