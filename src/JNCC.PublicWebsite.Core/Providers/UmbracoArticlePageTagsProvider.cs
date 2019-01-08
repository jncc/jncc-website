﻿using JNCC.PublicWebsite.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Cache;
using Umbraco.Core.Models;

namespace JNCC.PublicWebsite.Core.Providers
{
    internal sealed class UmbracoArticlePageTagsProvider : UmbracoArticlePagesProvider, ITagsProvider<IPublishedContent>
    {
        public IEnumerable<string> GetTagsByRoot(IPublishedContent root, string tagGroup)
        {
            var articlePages = GetArticlePages(root);

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