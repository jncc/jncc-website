using JNCC.PublicWebsite.Core.Attributes.Routing;
using JNCC.PublicWebsite.Core.Configuration;
using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using System;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class ResetPasswordFormSurfaceController : CoreSurfaceController
    {
        [ChildActionOnly]
        public ActionResult RenderForm()
        {
            if (TempData.HasSuccessFlag(SuccessFlags.CompletedResetPasswordSuccess))
            {
                return PartialView("~/Views/Partials/ResetPassword/Success.cshtml", new InitialResetPasswordModel());
            }

            return PartialView("~/Views/Partials/ResetPassword/InitialForm.cshtml", new InitialResetPasswordModel());
        }

        [ChildActionOnly]
        [ActionName("RenderForm")]
        [RequireParameter("requestToken")]
        public ActionResult RenderForm(string requestToken)
        {
            var config = ResetPasswordConfiguration.GetConfig();
            var requestService = new PetaPocoResetPasswordRequestService(ApplicationContext.DatabaseContext, config);
            var service = new ResetPasswordService(Services.MemberService, requestService);

            if (service.IsRequestTokenValid(requestToken) == false)
            {
                return PartialView("~/Views/Partials/ResetPassword/InvalidRequestToken.cshtml");
            }

            return PartialView("~/Views/Partials/ResetPassword/ResetPasswordForm.cshtml", new ResetPasswordModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PerformInitialResetPassword(InitialResetPasswordModel model)
        {
            if (ModelState.IsValid == false)
            {
                return CurrentUmbracoPage();
            }

            var existingMember = Services.MemberService.GetByEmail(model.EmailAddress);

            if (existingMember == null)
            {
                ModelState.AddModelError("Model", "Unable to find user with provided email address.");
                return CurrentUmbracoPage();
            }
            var config = ResetPasswordConfiguration.GetConfig();
            var requestService = new PetaPocoResetPasswordRequestService(ApplicationContext.DatabaseContext, config);
            var service = new ResetPasswordService(Services.MemberService, requestService);
            Guid token;

            if (service.TryCreateInitialRequest(existingMember, out token) == false)
            {
                ModelState.AddModelError("Model", "Unable to reset password at this time.");
                return CurrentUmbracoPage();
            }

            var notificationService = new ResetPasswordEmailNotificationService(config);
            notificationService.SendInitialRequestEmail(existingMember, token, CurrentPage);

            TempData.SetSuccessFlag(SuccessFlags.InitialResetPasswordRequestSuccess);

            return RedirectToCurrentUmbracoPage();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireParameter("requestToken")]
        public ActionResult PerformResetPassword(ResetPasswordModel model, string requestToken)
        {
            if (ModelState.IsValid == false)
            {
                return CurrentUmbracoPage();
            }

            var config = ResetPasswordConfiguration.GetConfig();
            var requestService = new PetaPocoResetPasswordRequestService(ApplicationContext.DatabaseContext, config);
            var service = new ResetPasswordService(Services.MemberService, requestService);

            var request = requestService.Get(requestToken);

            if (service.IsRequestTokenValid(requestToken) == false)
            {
                return PartialView("~/Views/Partials/ResetPassword/InvalidRequestToken.cshtml");
            }

            var existingMember = service.GetMemberByRequest(request);

            if (service.ResetPassword(existingMember, request, model.NewPassword) == false)
            {
                ModelState.AddModelError("Model", "Unable to reset password at this time.");
                return CurrentUmbracoPage();
            }

            var notificationService = new ResetPasswordEmailNotificationService(config);
            notificationService.SendCompletedRequestEmail(existingMember, CurrentPage);

            TempData.SetSuccessFlag(SuccessFlags.CompletedResetPasswordSuccess);

            return RedirectToCurrentUmbracoPage();
        }
    }
}
