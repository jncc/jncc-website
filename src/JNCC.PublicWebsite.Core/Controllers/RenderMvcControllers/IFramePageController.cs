using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.RenderMvcControllers
{
    public sealed class IFramePageController : RenderMvcController
    {
        private readonly NavigationItemService _navigationItemService;
        private readonly IFramePageService _iframePageService;

        public IFramePageController()
        {
            _navigationItemService = new NavigationItemService();
            _iframePageService = new IFramePageService(_navigationItemService);
        }

        public ActionResult Index(IFramePage model)
        {
            var viewModel = _iframePageService.GetViewModel(model, Request.Url);

            return CurrentTemplate(viewModel);
        }
    }
}
