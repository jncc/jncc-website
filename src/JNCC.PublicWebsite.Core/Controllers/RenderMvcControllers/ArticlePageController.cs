using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.RenderMvcControllers
{
    public sealed class ArticlePageController : RenderMvcController
    {
        private readonly ArticleService _articleService = new ArticleService();

        public ActionResult Index(ArticlePage model)
        {
            var viewModel = _articleService.GetViewModel(model);

            return CurrentTemplate(viewModel);
        }
    }
}
