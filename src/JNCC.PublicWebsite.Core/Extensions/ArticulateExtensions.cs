using Articulate.Models;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Providers;
using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Core.Cache;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Extensions
{
    public static class ArticulateExtensions
    {
        private static readonly ICacheProvider _defaultCacheProvider = ApplicationContext.Current.ApplicationCache.RequestCache;

        public static IEnumerable<string> AllCategories(this IMasterModel articulate, ICacheProvider cacheProvider = null)
        {
            var provider = new UmbracoBlogCategoriesProvider(cacheProvider ?? _defaultCacheProvider);

            return provider.GetAllCategoriesByRoot(articulate.BlogArchiveNode);
        }
        public static IEnumerable<string> AllTags(this IMasterModel articulate, ICacheProvider cacheProvider = null)
        {
            var provider = new UmbracoBlogTagsProvider(cacheProvider ?? _defaultCacheProvider);

            return provider.GetAllTagsByRoot(articulate.BlogArchiveNode);
        }
        public static StaffProfilePage StaffProfilePage(this PostModel post)
        {
            return post.GetPropertyValue<StaffProfilePage>("authorStaffProfilePage");
        }
    }
}
