using JNCC.PublicWebsite.Core.Models;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class PageIncludesService
    {
        public IHtmlString GetHeadIncludes(IGlobalIncludesComposition globalIncludes, IPageSpecificIncludesComposition pageSpecificIncludes)
        {
            var includesBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(globalIncludes.GlobalHeadIncludes) == false)
            {
                includesBuilder.Append(globalIncludes.GlobalHeadIncludes);
            }

            if (pageSpecificIncludes != null &&
                string.IsNullOrWhiteSpace(pageSpecificIncludes.PageSpecificHeadIncludes) == false)
            {
                includesBuilder.Append(pageSpecificIncludes.PageSpecificHeadIncludes);
            }

            return new MvcHtmlString(includesBuilder.ToString());
        }

        public IHtmlString GetBodyIncludes(IGlobalIncludesComposition globalIncludes, IPageSpecificIncludesComposition pageSpecificIncludes)
        {
            var includesBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(globalIncludes.GlobalBodyIncludes) == false)
            {
                includesBuilder.Append(globalIncludes.GlobalBodyIncludes);
            }

            if (pageSpecificIncludes != null &&
                string.IsNullOrWhiteSpace(pageSpecificIncludes.PageSpecificBodyIncludes) == false)
            {
                includesBuilder.Append(pageSpecificIncludes.PageSpecificBodyIncludes);
            }


            return new MvcHtmlString(includesBuilder.ToString());
        }
    }
}
