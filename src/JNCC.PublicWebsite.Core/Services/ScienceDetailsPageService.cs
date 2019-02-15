using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class ScienceDetailsPageService
    {
        public ScienceDetailsPageViewModel GetViewModel(ScienceDetailsPage model)
        {
            return new ScienceDetailsPageViewModel();
        }
    }
}
