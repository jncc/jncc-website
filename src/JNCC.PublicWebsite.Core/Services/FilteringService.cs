using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Providers;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JNCC.PublicWebsite.Core.Services
{
    internal abstract class FilteringService<TModel, TViewModel, TRoot> where TModel : FilteringModel
                                                                        where TViewModel : FilteringViewModel
    {
        protected readonly ITagsProvider _tagsProvider;
        public FilteringService(ITagsProvider tagsProvider)
        {
            _tagsProvider = tagsProvider;
        }

        public abstract TViewModel GetFilteringViewModel(TModel filteringModel, TRoot root);

        protected IReadOnlyDictionary<string, bool> GetFilters(IEnumerable<string> allFilters, string[] selectedFilters)
        {
            if (ExistenceUtility.IsNullOrEmpty(allFilters))
            {
                return null;
            }

            if (ExistenceUtility.IsNullOrEmpty(selectedFilters))
            {
                return allFilters.ToDictionary(x => x, x => false);
            }

            return allFilters.ToDictionary(x => x, x => selectedFilters.Contains(x, StringComparer.OrdinalIgnoreCase));
        }

        protected virtual IEnumerable<string> GetAllTeams()
        {
            return _tagsProvider.GetTags("Teams");
        }
    }
}