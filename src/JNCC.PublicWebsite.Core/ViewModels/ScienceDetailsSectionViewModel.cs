using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public abstract class ScienceDetailsSectionViewModel : ScienceDetailsSectionViewModelBase
    {
        public IEnumerable<ScienceDetailsSubSectionViewModel> SubSections { get; set; }
    }
}