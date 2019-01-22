using JNCC.PublicWebsite.Core.Models;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.RenderMvcControllers
{
    public sealed class ResetPasswordPageController : RenderMvcController
    {
        public ActionResult Index(ResetPasswordPage model)
        {
            if (Members.IsLoggedIn())
            {
                return Redirect("/");
            }

            return CurrentTemplate(model);
        }
    }
}
