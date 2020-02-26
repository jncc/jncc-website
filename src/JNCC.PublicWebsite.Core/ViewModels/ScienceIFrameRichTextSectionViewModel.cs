using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceIFrameRichTextSectionViewModel : ScienceIFrameSectionViewModel, IScienceIFrameRichTextSectionViewModel
    {
        public IHtmlString Content { get; set; }
    }
}
