using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.RenderMvcControllers
{
    public sealed class ScienceDetailsPageController : RenderMvcController
    {
        public ActionResult Index(ScienceDetailsPage model)
        {
            var service = new ScienceDetailsPageService();
            var viewModel = service.GetViewModel(model);

            return CurrentTemplate(viewModel);
        }
    }
}
