using JNCC.PublicWebsite.Core.Models;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.RenderMvcControllers
{
    public sealed class ChangePasswordPageController : RenderMvcController
    {
        public ActionResult Index(ChangePasswordPage model)
        {
            if (Members.IsLoggedIn() == false)
            {
                return Redirect("/");
            }

            return CurrentTemplate(model);
        }
    }
}
