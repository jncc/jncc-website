using System.Collections.Generic;
using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public interface IScienceDetailsImageRichTextSectionViewModel
    {
        ImageViewModel Image { get; set; }

        string ImagePosition { get; set; }

        IHtmlString Content { get; set; }
    }
}
