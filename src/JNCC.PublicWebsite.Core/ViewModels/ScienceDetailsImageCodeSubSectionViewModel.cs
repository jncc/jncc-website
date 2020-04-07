using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceDetailsImageCodeSubSectionViewModel : ScienceDetailsSubSectionViewModel, IScienceDetailsImageCodeSectionViewModel
    {
        public string ImageCode { get; set; }
        public string ImagePosition { get; set; }
        public IHtmlString Content { get; set; }
    }
}
