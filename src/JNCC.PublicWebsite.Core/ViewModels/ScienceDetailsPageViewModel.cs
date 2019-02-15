using System.Collections.Generic;
using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceDetailsPageViewModel
    {
        public IHtmlString Preamble { get; set; }
        public IEnumerable<ScienceDetailsSectionViewModel> Sections { get; set; }
    }
}
