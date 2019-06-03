using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceCategoryRichTextSubSectionViewModel : ScienceCategorySubSectionViewModel, IScienceCategoryRichTextSectionViewModel
    {
        public IHtmlString Content { get; set; }
    }
}