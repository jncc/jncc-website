using JNCC.PublicWebsite.Core.Models;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Cache;
using Umbraco.Core.Models;

namespace JNCC.PublicWebsite.Core.Providers
{
    internal sealed class UmbracoArticlePageTagsProvider : UmbracoPagesProvider<ArticlePage>, ITagsProvider<IPublishedContent>
    {
        public UmbracoArticlePageTagsProvider(ICacheProvider cacheProvider) : base(cacheProvider)
        {
        }

        public IEnumerable<string> GetTagsByRoot(IPublishedContent root, string tagGroup)
        {
            var articlePages = GetContentPages(root);

            switch (tagGroup)
            {
                case "Teams":
                    return articlePages.SelectMany(x => x.ArticleTeams)
                                       .Distinct()
                                       .OrderBy(x => x);
                default:
                    return Enumerable.Empty<string>();
            }

        }
    }
}
