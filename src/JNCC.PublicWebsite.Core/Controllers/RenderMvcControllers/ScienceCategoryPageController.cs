using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Providers;
using JNCC.PublicWebsite.Core.Services;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.RenderMvcControllers
{
    public sealed class ScienceCategoryPageController : RenderMvcController
    {
        public ActionResult Index(ScienceCategoryPage model)
        {
            var provider = new UmbracoScienceDetailsPageProvider(Umbraco, ApplicationContext.ApplicationCache.RequestCache);
            var service = new ScienceCategoryPageService(provider);
            var viewModel = service.GetViewModel(model);

            return CurrentTemplate(viewModel);
        }
    }
}
