using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Providers;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.RenderMvcControllers
{
    public class ScienceCategoryPageController : RenderMvcController
    {
        public ActionResult Index(ScienceCategoryPage model)
        {
            var provider = new UmbracoScienceDetailsPageProvider(Umbraco, ApplicationContext.ApplicationCache.RequestCache);
            var foundScienceDetailPages = provider.GetByCategory(model)
                                                  .GroupBy(x => x.Name.First())
                                                  .OrderBy(x => x.Key)
                                                  .ToDictionary(x => x.Key, x => x.Select(y => new NavigationItemViewModel()
                                                  {
                                                      Text = y.Name,
                                                      Url = y.Url
                                                  }));

            var viewModel = new ScienceCategoryPageViewModel()
            {
                CategorisedPages = foundScienceDetailPages
            };

            return CurrentTemplate(viewModel);
        }
    }
}
