using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Providers
{
    internal sealed class UmbracoArticleTypesProvider : IArticleTypesProvider<IPublishedContent>
    {
        public IEnumerable<string> GetAllByRoot(IPublishedContent root)
        {
            var children = root.Children<ArticlePage>();

            if (ExistenceUtility.IsNullOrEmpty(children))
            {
                return Enumerable.Empty<string>();
            }

            return children.Select(x => x.ArticleType)
                           .Distinct()
                           .OrderBy(x => x);
        }
    }
}
