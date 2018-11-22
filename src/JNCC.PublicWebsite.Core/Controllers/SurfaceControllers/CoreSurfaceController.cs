using JNCC.PublicWebsite.Core.Models;
using System.Web.Routing;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public abstract class CoreSurfaceController : SurfaceController
    {
        protected HomePage Root { get; private set; }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            Root = CurrentPage.AncestorOrSelf<HomePage>();
        }
    }
}
