using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public interface IScienceCategorySliderSectionViewModel
    {
        bool ShowBackground { get; set; }
        bool ShowTimelineArrows { get; set; }
        IHtmlString Content { get; set; }
        IEnumerable<ScienceSliderSchemaViewModel> SliderItems { get; set; }
    }
}
