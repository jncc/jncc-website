using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceLatestUpdatedPageItemViewModel
    {
        public string Title { get; set; }
        public IHtmlString Content { get; set; }
        public NavigationItemViewModel ReadMoreLink { get; set; }
    }
}