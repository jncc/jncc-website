using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class LogoutSurfaceController : SurfaceController
    {
        [HttpPost]
        public ActionResult PerformLogout()
        {
            if (Members.IsLoggedIn())
            {
                Members.Logout();
            }

            return Redirect("/");
        }
    }
}
