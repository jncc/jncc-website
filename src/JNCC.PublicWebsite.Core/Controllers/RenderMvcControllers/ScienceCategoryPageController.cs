using JNCC.PublicWebsite.Core.Models;
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
            var scienceDetailPages = Umbraco.TypedContentAtXPath("//scienceDetailsPage").OfType<ScienceDetailsPage>().ToArray();

            var foundScienceDetailPages = scienceDetailPages.Where(x => ExistenceUtility.IsNullOrEmpty(x.Categories) == false && x.Categories.Any(y => y.Id == model.Id))
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
