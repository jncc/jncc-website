using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceDetailsRichTextSubSectionViewModel : ScienceDetailsSubSectionViewModel, IScienceDetailsRichTextSectionViewModel
    {
        public IHtmlString Content { get; set; }
    }
}