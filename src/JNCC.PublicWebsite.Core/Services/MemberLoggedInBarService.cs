using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;

namespace JNCC.PublicWebsite.Core.Services
{
    internal class MemberLoggedInBarService
    {
        internal MemberLoggedInBarViewModel GetViewModel(HomePage root)
        {
            return new MemberLoggedInBarViewModel();
        }
    }
}
