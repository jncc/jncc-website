using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public interface IScienceDetailsImageGallerySectionViewModel
    {
        IEnumerable<ImageViewModel> Images { get; set; }
    }
}