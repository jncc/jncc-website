using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Web.ModelBinding;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.RenderMvcControllers
{
    public sealed class HowToApplyPageController : RenderMvcController
    {
        private readonly NavigationItemService _navigationItemService;
        private readonly CareersLandingPageService _careersLandingPageService;

        public HowToApplyPageController()
        {
            _navigationItemService = new NavigationItemService();
            _careersLandingPageService = new CareersLandingPageService(_navigationItemService);
        }

        public ActionResult Index(HowToApplyPage model, [QueryString]string id)
        {
            HowToApplyPageViewModel viewModel = new HowToApplyPageViewModel();

            viewModel.Content = model;
            viewModel.FeaturedJob = _careersLandingPageService.GetFeaturedJob(model.Parent as CareersLandingPage, id);

            return CurrentTemplate(viewModel);
        }
    }
}
