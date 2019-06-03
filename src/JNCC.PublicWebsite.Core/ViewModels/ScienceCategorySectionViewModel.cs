using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public abstract class ScienceCategorySectionViewModel : ScienceCategorySectionViewModelBase
    {
        public IEnumerable<ScienceCategorySubSectionViewModel> SubSections { get; set; }
    }
}