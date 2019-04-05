using JNCC.PublicWebsite.Core.Services;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class BreadcrumbsSurfaceController : CoreSurfaceController
    {
        private readonly NavigationItemService _navigationItemService = new NavigationItemService();

        [ChildActionOnly]
        public ActionResult RenderBreadcrumbs()
        {
            var visibleOrderedAncestors = CurrentPage.Ancestors()
                                                     .Where(x => x.IsVisible())
                                                     .OrderBy(x => x.Level)
                                                     .ToList();

            var viewModel = new BreadcrumbsViewModel()
            {
                Ancestors = _navigationItemService.GetViewModels(visibleOrderedAncestors),
                CurrentPage = CurrentPage.Name
            };

            return PartialView("~/Views/Partials/Breadcrumbs.cshtml", viewModel);
        }
    }
}
