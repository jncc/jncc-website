using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Cache;
using Umbraco.Core.Models;

namespace JNCC.PublicWebsite.Core.Providers
{
    internal sealed class UmbracoBlogTagsProvider : UmbracoPagesProvider<ArticulatePost>, IBlogTagsProvider<IPublishedContent>
    {
        public UmbracoBlogTagsProvider(ICacheProvider cacheProvider) : base(cacheProvider)
        {
        }

        public IEnumerable<string> GetAllTagsByRoot(IPublishedContent root)
        {
            var blogPosts = GetContentPages(root);

            if (ExistenceUtility.IsNullOrEmpty(blogPosts))
            {
                return Enumerable.Empty<string>();
            }

            return blogPosts.SelectMany(x => x.Tags).Distinct().OrderBy(x => x);
        }
    }
}
