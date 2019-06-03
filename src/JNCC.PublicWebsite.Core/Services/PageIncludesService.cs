using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class PageIncludesService
    {

        public static PageAttributesViewModel GetPageAttributesViewModel(IPageSpecificIncludesComposition pageSpecificIncludesComposition)
        {
            var viewmodel = new PageAttributesViewModel() {

                HTMLLangRef = pageSpecificIncludesComposition.HTmllangRef,
                NoIndex = pageSpecificIncludesComposition.NoIndex,
                LTRValue = pageSpecificIncludesComposition.GetCulture(),
            };

            return viewmodel;
        }

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
