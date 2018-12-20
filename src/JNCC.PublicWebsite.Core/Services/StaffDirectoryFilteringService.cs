using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Services;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class StaffDirectoryFilteringService : FilteringService<StaffDirectoryFilteringModel, StaffDirectoryFilteringViewModel>
    {
        public StaffDirectoryFilteringService(ITagService tagService) : base(tagService)
        {
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

        private IEnumerable<string> GetAllLocations()
        {
            return _tagService.GetAllTags("Locations")
                              .Where(x => x.NodeCount > 0)
                              .OrderBy(x => x.Text)
                              .Select(x => x.Text);
        }
    }
}
