﻿using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Cache;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Providers
{
    internal sealed class UmbracoArticleTypesProvider : UmbracoPagesProvider<ArticlePage>, IArticleTypesProvider<IPublishedContent>
    {
        public UmbracoArticleTypesProvider(ICacheProvider cacheProvider) : base(cacheProvider)
        {
        }

        public IEnumerable<string> GetAllByRoot(IPublishedContent root)
        {
            var articlePages = GetContentPages(root);

            if (ExistenceUtility.IsNullOrEmpty(articlePages))
            {
                return Enumerable.Empty<string>();
            }

            return articlePages.Select(x => x.ArticleType)
                               .Distinct()
                               .OrderBy(x => x);
        }
    }
}
