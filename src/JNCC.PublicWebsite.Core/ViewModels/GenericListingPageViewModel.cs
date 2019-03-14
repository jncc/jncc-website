using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class GenericListingPageViewModel : ListingViewModel<GenericListingPageItemViewModel>
    {
        public IHtmlString Preamble { get; set; }
        public IHtmlString PostListingContent { get; set; }
    }
}
