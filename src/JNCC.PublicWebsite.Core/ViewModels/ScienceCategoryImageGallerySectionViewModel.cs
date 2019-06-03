using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceCategoryImageGallerySectionViewModel : ScienceCategorySectionViewModel, IScienceCategoryImageGallerySectionViewModel
    {
        public IEnumerable<ImageGalleryItemViewModel> Images { get; set; }
    }
}
