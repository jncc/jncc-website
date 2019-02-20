using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Providers;
using JNCC.PublicWebsite.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var foundScienceDetailPages = _pagesProvider.GetByCategory(scienceCategoryPage)
                                                  .GroupBy(x => x.Name.First())
                                                  .OrderBy(x => x.Key)
                                                  .ToDictionary(x => x.Key, x => x.Select(y => new NavigationItemViewModel()
                                                  {
                                                      Text = y.Name,
                                                      Url = y.Url
                                                  }));

            return new ScienceCategoryPageViewModel()
            {
                CategorisedPages = foundScienceDetailPages
            };
        }
    }
}
