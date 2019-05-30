using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceCategoryImageGallerySubSectionViewModel : ScienceCategorySubSectionViewModel, IScienceCategoryImageGallerySectionViewModel
    {
        public IEnumerable<ImageGalleryItemViewModel> Images { get; set; }
    }
}