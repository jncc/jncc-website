using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class SeoMetaDataSurfaceController : CoreSurfaceController
    {
        private readonly SeoMetaDataService seoMetaDataService = new SeoMetaDataService();

        [ChildActionOnly]
        public ActionResult RenderSeoMetaData()
        {
            SeoMetaDataViewModel viewModel;

            if (CurrentPage is ISeoComposition)
            {
                viewModel = seoMetaDataService.GetViewModelFromSeoSettings(CurrentPage as ISeoComposition);
            }
            else
            {
                viewModel = seoMetaDataService.GetViewModel(CurrentPage);
            }

            return PartialView("~/Views/Partials/SeoMetaData.cshtml", viewModel);
        }
    }
}
