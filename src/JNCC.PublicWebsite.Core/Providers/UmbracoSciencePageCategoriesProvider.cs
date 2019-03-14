using JNCC.PublicWebsite.Core.Models;
using System.Collections.Generic;
using Umbraco.Core.Cache;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Providers
{
    internal sealed class UmbracoSciencePageCategoriesProvider : UmbracoPagesProvider<ScienceDetailsPage, ScienceCategoryPage>, ISciencePageCategoriesProvider
    {
        public UmbracoSciencePageCategoriesProvider(ICacheProvider cacheProvider) : base(cacheProvider)
        {
        }

        public IEnumerable<ScienceCategoryPage> GetCategories(ScienceDetailsPage content)
        {
            return GetContentPages(content);
        }

        protected override IEnumerable<ScienceCategoryPage> GetContentPagesForCaching(ScienceDetailsPage root)
        {
            return root.Categories;
        }
    }
}
