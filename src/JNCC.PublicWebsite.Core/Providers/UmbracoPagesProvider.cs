using System.Collections.Generic;
using Umbraco.Core.Cache;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Providers
{
    internal abstract class UmbracoPagesProvider<TRoot, TContent> where TRoot : class, IPublishedContent
                                                                  where TContent : class, IPublishedContent
    {
        private readonly ICacheProvider _cacheProvider;
        public UmbracoPagesProvider(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public virtual IEnumerable<TContent> GetContentPages(TRoot root)
        {
            var cacheKey = string.Format("ContentPages_For_Root_{0}", root.Id);

            return _cacheProvider.GetCacheItem<IEnumerable<TContent>>(cacheKey, () => GetContentPagesForCaching(root));
        }

        protected virtual IEnumerable<TContent> GetContentPagesForCaching(TRoot root)
        {
            return root.Children<TContent>();
        }
    }

    internal abstract class UmbracoPagesProvider<TContent> : UmbracoPagesProvider<IPublishedContent, TContent> where TContent : class, IPublishedContent
    {
        public UmbracoPagesProvider(ICacheProvider cacheProvider) : base(cacheProvider)
        {
        }
    }
}