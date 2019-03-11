using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceDetailsRichTextSubSectionViewModel : ScienceDetailsSubSectionViewModel
    {
        public IHtmlString Content { get; set; }
    }
}