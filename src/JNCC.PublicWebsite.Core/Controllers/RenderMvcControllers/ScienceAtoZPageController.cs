using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.RenderMvcControllers
{
    public sealed class ScienceAtoZPageController : RenderMvcController
    {
        private readonly ScienceAtoZPageService _scienceAtoZPageService = new ScienceAtoZPageService();

        public ActionResult Index(ScienceAtoZpage model)
        {
            var viewModel = _scienceAtoZPageService.GetViewModel(model);
            return CurrentTemplate(viewModel);
        }
    }
}
