using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceDetailsRichTextSectionViewModel : ScienceDetailsSectionViewModel
    {
        public IHtmlString Content { get; set; }
    }
}
