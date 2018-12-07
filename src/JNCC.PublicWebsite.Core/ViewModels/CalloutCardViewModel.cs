using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class CalloutCardViewModel
    {
        public string Title { get; set; }
        public IHtmlString Content { get; set; }
        public NavigationItemViewModel ReadMoreButton { get; set; }
        public ImageViewModel Image { get; set; }
    }
}