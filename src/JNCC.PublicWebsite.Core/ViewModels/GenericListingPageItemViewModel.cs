using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class GenericListingPageItemViewModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public IHtmlString Content { get; set; }
    }
}
