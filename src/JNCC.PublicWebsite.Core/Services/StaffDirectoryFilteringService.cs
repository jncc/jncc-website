using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Services;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class StaffDirectoryFilteringService
    {
        private ITagService _tagService;

        public StaffDirectoryFilteringService(ITagService tagService)
        {
            _tagService = tagService;
        }

        public StaffDirectoryFilteringViewModel GetFilteringViewModel(string[] locations, string[] teams)
        {
            var allLocations = GetAllLocations();
            var allTeams = GetAllTeams();

            return new StaffDirectoryFilteringViewModel()
            {
                Locations = GetFilters(allLocations, locations),
                Teams = GetFilters(allTeams, teams)
            };
        }

        private IReadOnlyDictionary<string, bool> GetFilters(IEnumerable<string> allFilters, string[] selectedFilters)
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

        private IEnumerable<string> GetAllLocations()
        {
            return _tagService.GetAllTags("Locations")
                              .Where(x => x.NodeCount > 0)
                              .OrderBy(x => x.Text)
                              .Select(x => x.Text);
        }
        private IEnumerable<string> GetAllTeams()
        {
            return _tagService.GetAllTags("Teams")
                              .Where(x => x.NodeCount > 0)
                              .OrderBy(x => x.Text)
                              .Select(x => x.Text);
        }
    }
}
