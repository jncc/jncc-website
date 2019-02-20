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
            return _pagesProvider.GetByCategory(scienceCategoryPage)
                                 .GroupBy(x => x.GetCategorisationCharacter())
                                 .OrderBy(x => x.Key)
                                 .ToDictionary(x => x.Key, x => x.Select(y => new NavigationItemViewModel()
                                 {
                                     Text = y.GetHeadline(),
                                     Url = y.Url
                                 }));
        }
        private IReadOnlyDictionary<char, IEnumerable<NavigationItemViewModel>> GetRelatedCategories(ScienceCategoryPage scienceCategoryPage)
        {
            if (ExistenceUtility.IsNullOrEmpty(scienceCategoryPage.RelatedCategories))
            {
                return null;
            }

            return scienceCategoryPage.RelatedCategories
                        .GroupBy(x => x.GetCategorisationCharacter())
                        .OrderBy(x => x.Key)
                        .ToDictionary(x => x.Key, x => x.Select(y => new NavigationItemViewModel()
                        {
                            Text = y.GetHeadline(),
                            Url = y.Url
                        }));
        }
    }
}
