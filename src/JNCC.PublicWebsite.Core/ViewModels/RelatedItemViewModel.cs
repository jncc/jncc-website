using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class RelatedItemViewModel
    {
        public string ImageUrl { get; set; }
        public IHtmlString Content { get; set; }
        public NavigationItemViewModel Link { get; set; }
    }
}
