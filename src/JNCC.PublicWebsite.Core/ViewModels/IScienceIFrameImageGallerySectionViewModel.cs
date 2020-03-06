using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public interface IScienceIFrameImageGallerySectionViewModel
    {
        IEnumerable<ImageGalleryItemViewModel> Images { get; set; }
    }
}