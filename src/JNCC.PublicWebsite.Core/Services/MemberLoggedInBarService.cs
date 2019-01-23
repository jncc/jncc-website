using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class MemberLoggedInBarService
    {
        internal MemberLoggedInBarViewModel GetViewModel(HomePage root)
        {
            var viewModel = new MemberLoggedInBarViewModel();

            if (root.MemberAccountPage != null)
            {
                viewModel.MemberAccountPageUrl = root.MemberAccountPage.Url;
            }

            return viewModel;
        }
    }
}
