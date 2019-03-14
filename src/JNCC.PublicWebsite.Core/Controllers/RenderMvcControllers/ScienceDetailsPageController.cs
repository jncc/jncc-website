using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using JNCC.PublicWebsite.Core.Providers;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.RenderMvcControllers
{
    public sealed class ScienceDetailsPageController : RenderMvcController
    {
        private readonly NavigationItemService _navigationService = new NavigationItemService();

        public ActionResult Index(ScienceDetailsPage model)
        {
            var categoriesProvider = new UmbracoSciencePageCategoriesProvider(ApplicationContext.ApplicationCache.RequestCache);
            var service = new ScienceDetailsPageService(categoriesProvider, _navigationService);
            var viewModel = service.GetViewModel(model);

            return CurrentTemplate(viewModel);
        }
    }
}
