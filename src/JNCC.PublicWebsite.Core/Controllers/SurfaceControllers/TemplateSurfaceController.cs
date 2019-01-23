using System.Web.Mvc;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class TemplateSurfaceController : CoreSurfaceController
    {
        [ChildActionOnly]
        public ActionResult RenderTemplateName()
        {
            return Content(CurrentPage.GetTemplateAlias().ToString());
        }
    }
}
