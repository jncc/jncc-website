using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;

namespace JNCC.PublicWebsite.Core.Services
{
    internal class MemberLoggedInBarService
    {
        internal MemberLoggedInBarViewModel GetViewModel(HomePage root)
        {
            var viewModel = new MemberLoggedInBarViewModel();

            if (root.ChangePasswordPage != null)
            {
                viewModel.ChangePasswordPageUrl = root.ChangePasswordPage.Url;
            }

            return viewModel;
        }
    }
}
