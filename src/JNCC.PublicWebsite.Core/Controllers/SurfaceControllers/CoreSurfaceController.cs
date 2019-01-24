using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Models;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public abstract class CoreSurfaceController : SurfaceController
    {
        private static readonly EmptyResult emptyResult = new EmptyResult();
        protected HomePage Root { get; private set; }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            Root = CurrentPage.Site<HomePage>();
        }

        protected EmptyResult EmptyResult()
        {
            return emptyResult;
        }

        protected override IPublishedContent CurrentPage
        {
            get
            {
                return Umbraco.AssignedContentItem;
            }
        }
    }
}
