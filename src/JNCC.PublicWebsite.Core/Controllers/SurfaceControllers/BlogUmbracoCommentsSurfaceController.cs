using JNCC.PublicWebsite.Core.Services;
using JNCC.PublicWebsite.Core.Utilities;
using System;
using System.Web.Mvc;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class BlogUmbracoCommentsSurfaceController : CoreSurfaceController
    {
        [ChildActionOnly]
        public ActionResult RenderCommentsList()
        {
            var service = new UmbracoFormsBlogCommentsService();

            var formId = CurrentPage.GetPropertyValue<Guid>("commentsForm", true);

            if (formId == default(Guid))
            {
                return EmptyResult();
            }

            var comments = service.GetComments(CurrentPage.Id, formId);

            if (ExistenceUtility.IsNullOrEmpty(comments))
            {
                return EmptyResult();
            }

            return PartialView("~/Views/Partials/Blog/CommentsList.cshtml", comments);
        }
    }
}
