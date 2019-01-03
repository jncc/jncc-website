using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Providers;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using Umbraco.Core.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class StaffDirectoryFilteringService : FilteringService<StaffDirectoryFilteringModel, StaffDirectoryFilteringViewModel, IPublishedContent>
    {
        public StaffDirectoryFilteringService(ITagsProvider tagsProvider) : base(tagsProvider)
        {
        }

        public override StaffDirectoryFilteringViewModel GetFilteringViewModel(StaffDirectoryFilteringModel filteringModel, IPublishedContent root)
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
            return _tagsProvider.GetTags("Locations");
        }
    }
}
