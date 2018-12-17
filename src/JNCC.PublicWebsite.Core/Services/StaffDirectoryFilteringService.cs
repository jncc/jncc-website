using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Services;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class StaffDirectoryFilteringService : FilteringService<StaffDirectoryFilteringModel, StaffDirectoryFilteringViewModel>
    {
        private ITagService _tagService;

        public StaffDirectoryFilteringService(ITagService tagService)
        {
            _tagService = tagService;
        }

        public override StaffDirectoryFilteringViewModel GetFilteringViewModel(StaffDirectoryFilteringModel filteringModel)
        {
            var allLocations = GetAllLocations();
            var allTeams = GetAllTeams();

            var viewModel = new StaffDirectoryFilteringViewModel()
            {
                Locations = GetFilters(allLocations, filteringModel.Locations),
                Teams = GetFilters(allTeams, filteringModel.Teams)
            };

            if (string.IsNullOrWhiteSpace(filteringModel.SearchTerm) == false)
            {
                viewModel.SearchTerm = filteringModel.SearchTerm;
            }

            return viewModel;
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
