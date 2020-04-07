using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceCategorySliderSectionViewModel : ScienceCategorySectionViewModel, IScienceCategorySliderSectionViewModel
    {
        public bool ShowBackground { get; set; }
        public bool ShowTimelineArrows { get; set; }
        public IHtmlString Content { get; set; }
        public IEnumerable<ScienceSliderSchemaViewModel> SliderItems { get; set; }
    }
}
