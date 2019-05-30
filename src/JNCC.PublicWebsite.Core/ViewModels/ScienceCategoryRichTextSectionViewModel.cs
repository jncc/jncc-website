using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceCategoryRichTextSectionViewModel : ScienceCategorySectionViewModel, IScienceCategoryRichTextSectionViewModel
    {
        public IHtmlString Content { get; set; }
    }
}
