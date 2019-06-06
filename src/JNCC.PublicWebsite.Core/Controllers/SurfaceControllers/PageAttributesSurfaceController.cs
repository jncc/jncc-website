using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Web.Mvc;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class PageAttributesSurfaceController : CoreSurfaceController
    {
        [ChildActionOnly]
        public ActionResult RenderAttributes()
        {

            PageAttributesViewModel viewModel = new PageAttributesViewModel();

            if (CurrentPage is IPageSpecificIncludesComposition)
            {
                viewModel = PageIncludesService.GetPageAttributesViewModel(CurrentPage as IPageSpecificIncludesComposition);
            }

            return PartialView("~/Views/Partials/PageAttributes.cshtml", viewModel);
        }
    }
}
