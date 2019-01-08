using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public class TemplateSurfaceController : CoreSurfaceController
    {
        public ActionResult RenderTemplateName()
        {
            return Content(CurrentPage.GetTemplateAlias().ToString());
        }
    }
}
