using Articulate.Models;
using JNCC.PublicWebsite.Core.Providers;
using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Core.Cache;

namespace JNCC.PublicWebsite.Core.Extensions
{
    public static class ArticulateExtensions
    {
        public static IEnumerable<string> AllCategories(this IMasterModel articulate, ICacheProvider cacheProvider = null)
        {
            var provider = new UmbracoBlogCategoriesProvider(cacheProvider ?? ApplicationContext.Current.ApplicationCache.RequestCache);

            return provider.GetAllCategoriesByRoot(articulate.BlogArchiveNode);
        }
    }
}
