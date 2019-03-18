using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Models;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class PageMetaInformationSurfaceController : CoreSurfaceController
    {
        [ChildActionOnly]
        public ActionResult RenderPageMetaInformation()
        {
            if (CurrentPage is IPageMetaInformationComposition)
            {
                return EmptyResult();
            }

            var viewModel = (CurrentPage as IPageMetaInformationComposition).GetPublishedDateOrDefault();

            return PartialView("~/Views/Partials/PageMetaInformation.cshtml", viewModel);
        }
    }
}
