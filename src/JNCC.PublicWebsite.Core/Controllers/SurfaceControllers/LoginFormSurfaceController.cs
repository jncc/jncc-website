using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class LoginFormSurfaceController : SurfaceController
    {
        [HttpGet]
        [ChildActionOnly]
        public ActionResult RenderLoginForm()
        {
            return PartialView("~/Views/Partials/LoginForm.cshtml");
        }

    }
}
