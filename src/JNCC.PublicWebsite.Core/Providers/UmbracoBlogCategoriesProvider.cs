using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Cache;
using Umbraco.Core.Models;

namespace JNCC.PublicWebsite.Core.Providers
{
    internal sealed class UmbracoBlogCategoriesProvider : UmbracoPagesProvider<ArticulatePost>, IBlogCategoriesProvider<IPublishedContent>
    {
        public UmbracoBlogCategoriesProvider(ICacheProvider cacheProvider) : base(cacheProvider)
        {
        }

        public IEnumerable<string> GetAllCategoriesByRoot(IPublishedContent root)
        {
            var blogPosts = GetContentPages(root);

            if (ExistenceUtility.IsNullOrEmpty(blogPosts))
            {
                return Enumerable.Empty<string>();
            }

            return blogPosts.SelectMany(x => x.Categories).Distinct();
        }
    }
}
