using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public abstract class ScienceIFrameSectionViewModel : ScienceIFrameSectionViewModelBase
    {
        public IEnumerable<ScienceIFrameSubSectionViewModel> SubSections { get; set; }
    }
}