using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceDetailsImageRichTextSubSectionViewModel : ScienceDetailsSubSectionViewModel, IScienceDetailsImageRichTextSectionViewModel
    {
        public ImageViewModel Image { get; set; }
        public IEnumerable<int> ImagePosition { get; set; }
        public IHtmlString Content { get; set; }
    }
}
