using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class IFramePageViewModel
    {
        public MainNavigationViewModel Navigation { get; set; }
        public string SourceUrl { get; set; }
        public IHtmlString CookieError { get; set; }
    }
}
