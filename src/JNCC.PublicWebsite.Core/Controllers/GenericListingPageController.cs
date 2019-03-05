using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers
{
    public sealed class GenericListingPageController : RenderMvcController
    {
        public ActionResult Index(GenericListingPage model, FilteringModel filteringModel)
        {
            var service = new GenericListingPageService();
            var viewModel = service.GetViewModel(model, filteringModel);

            return CurrentTemplate(viewModel);
        }
    }
}
