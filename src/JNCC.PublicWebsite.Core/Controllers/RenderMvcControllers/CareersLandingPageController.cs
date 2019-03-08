using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.RenderMvcControllers
{
    public sealed class CareersLandingPageController : RenderMvcController
    {
        private readonly CareersLandingPageService _careersLandingPageService = new CareersLandingPageService();

        public ActionResult Index(CareersLandingPage model)
        {
            var viewModel = _careersLandingPageService.GetViewModel(model);

            return CurrentTemplate(viewModel);
        }
    }
}
