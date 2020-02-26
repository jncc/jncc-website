using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using System.Collections.Generic;
using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class IFramePageViewModel
    {
        public MainNavigationViewModel Navigation { get; set; }
        public string SourceUrl { get; set; }
        public IHtmlString CookieError { get; set; }
        public string Headline { get; set; }
        public IHtmlString Preamble { get; set; }
        public IEnumerable<ScienceIFrameSectionViewModel> Sections { get; set; }
        public bool HasSections
        {
            get
            {
                return ExistenceUtility.IsNullOrEmpty(Sections) == false;
            }
        }
        public bool showOnMedium { get; set; }
        public bool showOnSmall { get; set; }
        public IFramePage Content { get; set; }

    }
}
