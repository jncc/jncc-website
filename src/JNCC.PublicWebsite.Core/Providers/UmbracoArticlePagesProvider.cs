using JNCC.PublicWebsite.Core.Models;
using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Providers
{
    internal abstract class UmbracoArticlePagesProvider
    {
        protected IEnumerable<ArticlePage> GetArticlePages(IPublishedContent root)
        {
            return root.Children<ArticlePage>();
        }
    }
}