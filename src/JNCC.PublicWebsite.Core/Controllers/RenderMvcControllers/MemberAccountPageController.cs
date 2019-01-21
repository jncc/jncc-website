using JNCC.PublicWebsite.Core.Models;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.RenderMvcControllers
{
    public sealed class MemberAccountPageController : RenderMvcController
    {
        public ActionResult Index(MemberAccountPage model)
        {
            if (Members.IsLoggedIn() == false)
            {
                return Redirect("/");
            }

            return CurrentTemplate(model);
        }
    }
}
