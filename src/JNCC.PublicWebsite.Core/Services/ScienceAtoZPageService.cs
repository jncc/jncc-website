using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class ScienceAtoZPageService
    {
        public ScienceAtoZPageService()
        {
        }

        public ScienceAtoZPageViewModel GetViewModel(ScienceAtoZpage model)
        {
            return new ScienceAtoZPageViewModel();
        }
    }
}
