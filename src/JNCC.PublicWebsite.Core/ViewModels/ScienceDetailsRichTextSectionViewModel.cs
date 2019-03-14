using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceDetailsRichTextSectionViewModel : ScienceDetailsSectionViewModel, IScienceDetailsRichTextSectionViewModel
    {
        public IHtmlString Content { get; set; }
    }
}
