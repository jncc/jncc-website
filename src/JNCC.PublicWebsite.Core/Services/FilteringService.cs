using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Services;

namespace JNCC.PublicWebsite.Core.Services
{
    internal abstract class FilteringService<TModel, TViewModel> where TModel : FilteringModel
                                                                 where TViewModel : FilteringViewModel
    {
        protected readonly ITagService _tagService;
        public FilteringService(ITagService tagService)
        {
            _tagService = tagService;
        }

        public abstract TViewModel GetFilteringViewModel(TModel filteringModel);

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
            return _tagService.GetAllTags("Teams")
                              .Where(x => x.NodeCount > 0)
                              .OrderBy(x => x.Text)
                              .Select(x => x.Text);
        }
    }
}