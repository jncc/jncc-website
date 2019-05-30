using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public interface IScienceCategoryImageGallerySectionViewModel
    {
        IEnumerable<ImageGalleryItemViewModel> Images { get; set; }
    }
}