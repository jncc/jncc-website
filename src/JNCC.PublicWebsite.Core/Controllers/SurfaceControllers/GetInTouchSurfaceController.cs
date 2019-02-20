using JNCC.PublicWebsite.Core.Models;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class GetInTouchSurfaceController : CoreSurfaceController
    {
        [ChildActionOnly]
        public ActionResult RenderGetInTouch()
        {
            if (CurrentPage is IGetInTouchComposition == false)
            {
                return EmptyResult();
            }

            return PartialView("~/Views/Partials/GetInTouch.cshtml", CurrentPage);
        }
    }
}
