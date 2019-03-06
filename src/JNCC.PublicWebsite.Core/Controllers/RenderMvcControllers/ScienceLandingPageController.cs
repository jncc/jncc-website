using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.RenderMvcControllers
{
    public sealed class ScienceLandingPageController : RenderMvcController
    {
        private readonly NavigationItemService _navigationItemService;
        private readonly CalloutCardsService _calloutCardsService;
        private readonly ScienceLandingPageService _scienceLandingPageService;

        public ScienceLandingPageController()
        {
            _navigationItemService = new NavigationItemService();
            _calloutCardsService = new CalloutCardsService(_navigationItemService);
            _scienceLandingPageService = new ScienceLandingPageService(_calloutCardsService);
        }

        public ActionResult Index(ScienceLandingPage model)
        {
            var viewModel = _scienceLandingPageService.GetViewModel(model);

            return CurrentTemplate(viewModel);
        }
    }
}
