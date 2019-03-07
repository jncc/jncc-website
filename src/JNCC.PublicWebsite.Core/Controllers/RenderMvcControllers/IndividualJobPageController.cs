using JNCC.PublicWebsite.Core.Controllers.Services;
using JNCC.PublicWebsite.Core.Models;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.RenderMvcControllers
{
    public sealed class IndividualJobPageController : RenderMvcController
    {
        private readonly IndividualJobPageService _individualJobPageService = new IndividualJobPageService();

        public ActionResult Index(IndividualJobPage model)
        {
            var viewModel = _individualJobPageService.GetViewModel(model);

            return CurrentTemplate(viewModel);
        }
    }
}
