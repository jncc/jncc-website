using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Models;
using System.Web.Mvc;
using Umbraco.Core.Logging;
using Umbraco.Web.Models;
using UmbConstants = Umbraco.Core.Constants;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class ChangePasswordFormSurfaceController : CoreSurfaceController
    {
        [ChildActionOnly]
        public ActionResult RenderChangePasswordForm()
        {
            return PartialView("~/Views/Partials/ChangePasswordForm.cshtml", new ChangePasswordModel());
        }

        [HttpPost]
        [ChildActionOnly]
        [ValidateAntiForgeryToken]
        [ActionName("RenderChangePasswordForm")]
        public ActionResult RenderPostChangePasswordForm(ChangePasswordModel model)
        {
            return PartialView("~/Views/Partials/ChangePasswordForm.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PerformChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid == false)
            {
                return CurrentUmbracoPage();
            }

            var changingPasswordModel = new ChangingPasswordModel()
            {
                NewPassword = model.NewPassword,
                OldPassword = model.OldPassword
            };

            var attempt = Members.ChangePassword(Members.CurrentUserName, changingPasswordModel, UmbConstants.Conventions.Member.UmbracoMemberProviderName);

            if (attempt.Success == false)
            {
                if (attempt.Exception != null)
                {
                    ModelState.AddModelError("Model", attempt.Exception);
                    LogHelper.Error<ChangePasswordFormSurfaceController>("Unable to change password", attempt.Exception);
                }
                else
                {
                    ModelState.AddModelError("Model", "Unable to change password at this time. Ensure your credentials are correct.");
                }

                return CurrentUmbracoPage();
            }
            TempData.SetSuccessFlag();

            return RedirectToCurrentUmbracoPage();
        }
    }
}
