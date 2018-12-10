using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ResourceItemViewModel
    {
        public string ImageUrl { get; set; }
        public IHtmlString Content { get; set; }
        public NavigationItemViewModel ReadMoreButton { get; set; }
    }
}