using JNCC.PublicWebsite.Core.Models;
using System.Collections.Generic;
using Umbraco.Core.Cache;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Providers
{
    internal abstract class UmbracoArticlePagesProvider
    {
        private readonly ICacheProvider _cacheProvider;
        public UmbracoArticlePagesProvider(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        protected IEnumerable<ArticlePage> GetArticlePages(IPublishedContent root)
        {
            var cacheKey = string.Format("ArticlePages_For_Root_{0}", root.Id);

            return _cacheProvider.GetCacheItem<IEnumerable<ArticlePage>>(cacheKey, () => root.Children<ArticlePage>());
        }
    }
}