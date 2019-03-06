using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.RenderMvcControllers
{
    public sealed class ScienceLandingPageController : RenderMvcController
    {
        private readonly ScienceLandingPageService _scienceLandingPageService;

        public ScienceLandingPageController()
        {
            _scienceLandingPageService = new ScienceLandingPageService();
        }

        public ActionResult Index(ScienceLandingPage model)
        {
            var viewModel = _scienceLandingPageService.GetViewModel(model);

            return CurrentTemplate(viewModel);
        }
    }
}
