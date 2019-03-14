using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Providers;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class ScienceCategoryPageService
    {
        private IScienceDetailsPageProvider _pagesProvider;

        public ScienceCategoryPageService(IScienceDetailsPageProvider pagesProvider)
        {
            _pagesProvider = pagesProvider;
        }

        public ScienceCategoryPageViewModel GetViewModel(ScienceCategoryPage scienceCategoryPage)
        {
            return new ScienceCategoryPageViewModel()
            {
                Headline = scienceCategoryPage.GetHeadline(),
                Preamble = scienceCategoryPage.Preamble,
                CategorisedPages = GetCategorisedPages(scienceCategoryPage),
                RelatedCategories = GetRelatedCategories(scienceCategoryPage)
            };
        }

        private IReadOnlyDictionary<char, IEnumerable<NavigationItemViewModel>> GetCategorisedPages(ScienceCategoryPage scienceCategoryPage)
        {
            var pages = _pagesProvider.GetByCategory(scienceCategoryPage);

            return CategorisePages(pages);
        }

        private IReadOnlyDictionary<char, IEnumerable<NavigationItemViewModel>> GetRelatedCategories(ScienceCategoryPage scienceCategoryPage)
        {
            return CategorisePages(scienceCategoryPage.RelatedCategories);
        }

        private IReadOnlyDictionary<char, IEnumerable<NavigationItemViewModel>> CategorisePages<T>(IEnumerable<T> pages) where T : ISciencePageCategorisationComposition, IPageHeroComposition
        {
            if (ExistenceUtility.IsNullOrEmpty(pages))
            {
                return null;
            }

            return pages.GroupBy(x => x.GetCategorisationCharacter())
                        .OrderBy(x => x.Key)
                        .ToDictionary(x => x.Key, x => x.Select(y => new NavigationItemViewModel()
                        {
                            Text = y.GetHeadline(),
                            Url = y.Url
                        }));
        }

    }
}
