using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.RenderMvcControllers
{
    public sealed class StaffProfilePageController : RenderMvcController
    {
        private readonly StaffProfileService _staffProfileService = new StaffProfileService();

        public ActionResult Index(StaffProfilePage model)
        {
            var viewModel = _staffProfileService.GetViewModel(model);

            return CurrentTemplate(viewModel);
        }
    }
}
