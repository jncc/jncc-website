using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceIFrameRichTextSubSectionViewModel : ScienceIFrameSubSectionViewModel, IScienceIFrameRichTextSectionViewModel
    {
        public IHtmlString Content { get; set; }
    }
}