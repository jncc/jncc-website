using System.Collections.Generic;
using Umbraco.Core.Cache;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Providers
{
    internal abstract class UmbracoPagesProvider<TContent> where TContent : class, IPublishedContent
    {
        private readonly ICacheProvider _cacheProvider;
        public UmbracoPagesProvider(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public virtual IEnumerable<TContent> GetContentPages(IPublishedContent root)
        {
            var cacheKey = string.Format("ContentPages_For_Root_{0}", root.Id);

            return _cacheProvider.GetCacheItem<IEnumerable<TContent>>(cacheKey, () => root.Children<TContent>());
        }
    }
}