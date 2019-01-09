using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using System.Web;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class PageIncludesSurfaceController : CoreSurfaceController
    {
        private readonly PageIncludesService _pageIncludesService = new PageIncludesService();

        [ChildActionOnly]
        public IHtmlString RenderHeadIncludes()
        {
            return _pageIncludesService.GetHeadIncludes(Root, CurrentPage as IPageSpecificIncludesComposition);
        }

        [ChildActionOnly]
        public IHtmlString RenderBodyIncludes()
        {
            return _pageIncludesService.GetBodyIncludes(Root, CurrentPage as IPageSpecificIncludesComposition);
        }
    }
}
