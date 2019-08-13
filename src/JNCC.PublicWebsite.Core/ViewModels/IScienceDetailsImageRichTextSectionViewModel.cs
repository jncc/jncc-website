using System.Collections.Generic;
using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public interface IScienceDetailsImageRichTextSectionViewModel
    {
        ImageViewModel Image { get; set; }

        IEnumerable<int> ImagePosition { get; set; }

        IHtmlString Content { get; set; }
    }
}
