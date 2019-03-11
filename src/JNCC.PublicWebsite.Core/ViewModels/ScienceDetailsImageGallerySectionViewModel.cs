using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceDetailsImageGallerySectionViewModel : ScienceDetailsSectionViewModel, IScienceDetailsImageGallerySectionViewModel
    {
        public IEnumerable<ImageViewModel> Images { get; set; }
    }
}
