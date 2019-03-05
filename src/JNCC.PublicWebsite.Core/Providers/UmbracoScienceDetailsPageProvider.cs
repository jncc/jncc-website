using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Cache;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Providers
{
    internal sealed class UmbracoScienceDetailsPageProvider : UmbracoPagesProvider<ScienceCategoryPage, ScienceDetailsPage>, IScienceDetailsPageProvider 
    {
        private readonly UmbracoHelper _umbracoHelper;

        public UmbracoScienceDetailsPageProvider(UmbracoHelper umbracoHelper, ICacheProvider cacheProvider) : base(cacheProvider)
        {
            _umbracoHelper = umbracoHelper;
        }

        public IEnumerable<ScienceDetailsPage> GetByCategory(ScienceCategoryPage scienceCategoryPage)
        {
            return GetContentPages(scienceCategoryPage);
        }

        protected override IEnumerable<ScienceDetailsPage> GetContentPagesForCaching(ScienceCategoryPage root)
        {
            var allScienceDetailPages = _umbracoHelper.TypedContentAtXPath("//scienceDetailsPage")
                                                   .OfType<ScienceDetailsPage>()
                                                   .ToArray();

            return allScienceDetailPages.Where(x => ExistenceUtility.IsNullOrEmpty(x.Categories) == false && x.Categories.Any(y => y.Id == root.Id));
        }
    }
}
