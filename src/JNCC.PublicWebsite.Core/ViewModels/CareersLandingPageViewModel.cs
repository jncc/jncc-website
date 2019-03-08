using System.Collections.Generic;
using System.Web;
using Umbraco.Core.Models;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class CareersLandingPageViewModel
    {
        public IHtmlString Preamble { get; set; }
        public IEnumerable<IPublishedContent> MainContent { get; set; }
        public IEnumerable<CareersListItemViewModel> Careers { get; set; }
    }
}
