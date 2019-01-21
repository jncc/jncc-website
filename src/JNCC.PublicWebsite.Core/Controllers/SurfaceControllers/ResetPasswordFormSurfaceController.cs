using JNCC.PublicWebsite.Core.Attributes.Routing;
using JNCC.PublicWebsite.Core.Configuration;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using JNCC.PublicWebsite.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class ResetPasswordFormSurfaceController : CoreSurfaceController
    {
        [ChildActionOnly]
        public ActionResult RenderForm()
        {
            return PartialView("~/Views/Partials/ResetPassword/InitialForm.cshtml", new InitialResetPasswordModel());
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

            TempData.Add("InitialRequestSuccess", true);

            return RedirectToCurrentUmbracoPage();
        }
    }
}
