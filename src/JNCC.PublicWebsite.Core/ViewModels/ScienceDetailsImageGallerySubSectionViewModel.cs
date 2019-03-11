using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceDetailsImageGallerySubSectionViewModel : ScienceDetailsSubSectionViewModel, IScienceDetailsImageGallerySectionViewModel
    {
        public IEnumerable<ImageGalleryItemViewModel> Images { get; set; }
    }
}