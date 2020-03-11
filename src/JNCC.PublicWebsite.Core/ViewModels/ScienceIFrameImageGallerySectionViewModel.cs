using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceIFrameImageGallerySectionViewModel : ScienceIFrameSectionViewModel, IScienceIFrameImageGallerySectionViewModel
    {
        public IEnumerable<ImageGalleryItemViewModel> Images { get; set; }
    }
}
