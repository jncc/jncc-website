using JNCC.PublicWebsite.Core.Models;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class PageIncludesSurfaceController : CoreSurfaceController
    {
        [ChildActionOnly]
        public IHtmlString RenderHeadIncludes()
        {
            var includesBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(Root.GlobalHeadIncludes) == false)
            {
                includesBuilder.Append(Root.GlobalHeadIncludes);
            }

            if (CurrentPage is IPageSpecificIncludesComposition)
            {
                var value = (CurrentPage as IPageSpecificIncludesComposition).PageSpecificHeadIncludes;

                if (string.IsNullOrWhiteSpace(value) == false)
                {
                    includesBuilder.Append(value);
                }
            }

            return new MvcHtmlString(includesBuilder.ToString());
        }

        [ChildActionOnly]
        public IHtmlString RenderBodyIncludes()
        {
            var includesBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(Root.GlobalBodyIncludes) == false)
            {
                includesBuilder.Append(Root.GlobalBodyIncludes);
            }

            if (CurrentPage is IPageSpecificIncludesComposition)
            {
                var value = (CurrentPage as IPageSpecificIncludesComposition).PageSpecificBodyIncludes;

                if (string.IsNullOrWhiteSpace(value) == false)
                {
                    includesBuilder.Append(value);
                }
            }

            return new MvcHtmlString(includesBuilder.ToString());
        }
    }
}
