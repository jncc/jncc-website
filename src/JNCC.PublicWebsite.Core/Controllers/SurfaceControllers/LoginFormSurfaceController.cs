using JNCC.PublicWebsite.Core.Attributes.Filters;
using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Models;
using System;
using System.Web.Mvc;
using System.Web.Security;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class LoginFormSurfaceController : SurfaceController
    {
        // Possibly add this to AppSettings or CMS eventually
        private const int RememberMeLoginCookieDurationInMinutes = 43200;
        private const int NonRememberMeLoginCookieDurationInMinutes = 120;

        [HttpGet]
        [ChildActionOnly]
        public ActionResult RenderLoginForm()
        {
            var model = new LoginFormModel();

            return PartialView("~/Views/Partials/LoginForm.cshtml", model);
        }

        [HttpPost]
        [ChildActionOnly]
        [ValidateAntiForgeryToken]
        [ActionName("RenderLoginForm")]
        [AllowXRequestsEveryXSeconds(Name = nameof(RenderPostLoginForm), Requests = RequestThrottling.NumberOfRequests, Seconds = RequestThrottling.NumberOfSeconds)]
        public ActionResult RenderPostLoginForm(LoginFormModel model)
        {
            return PartialView("~/Views/Partials/LoginForm.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowXRequestsEveryXSeconds(Name = nameof(PerformLogin), Requests = RequestThrottling.NumberOfRequests, Seconds = RequestThrottling.NumberOfSeconds)]
        public ActionResult PerformLogin(LoginFormModel model)
        {
            if (ModelState.IsValid == false)
            {
                return CurrentUmbracoPage();
            }

            if (Members.Login(model.Username, model.Password) == false)
            {
                ModelState.AddModelError("Model", "The Username/Password combination provided is not a valid login.");
                return CurrentUmbracoPage();
            }

            if (Response.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                var cookieDurationInMinutes = model.RememberMe ? RememberMeLoginCookieDurationInMinutes : NonRememberMeLoginCookieDurationInMinutes;

                Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddMinutes(cookieDurationInMinutes);
            }

            if (Request.RawUrl == CurrentPage.Url)
            {
                return Redirect("/");
            }

            return RedirectToCurrentUmbracoUrl();
        }
    }
}
