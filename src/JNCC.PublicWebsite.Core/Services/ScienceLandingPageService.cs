using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class ScienceLandingPageService
    {
        public ScienceLandingPageViewModel GetViewModel(ScienceLandingPage model)
        {
            return new ScienceLandingPageViewModel();
        }
    }
}
