using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceDetailsImageGallerySubSectionViewModel : ScienceDetailsSubSectionViewModel, IScienceDetailsImageGallerySectionViewModel
    {
        public IEnumerable<ImageViewModel> Images { get; set; }
    }
}