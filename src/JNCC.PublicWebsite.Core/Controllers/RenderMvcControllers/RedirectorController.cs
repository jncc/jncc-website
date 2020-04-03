using JNCC.PublicWebsite.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.RenderMvcControllers
{
    public sealed class RedirectorController : RenderMvcController
    {
        public ActionResult Index(Redirector model)
        {
            return RedirectPermanent(model.Redirect.Url);
        }
    }
}
