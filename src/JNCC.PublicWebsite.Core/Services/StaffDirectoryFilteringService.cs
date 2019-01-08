using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Providers;
using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Core.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class StaffDirectoryFilteringService : FilteringService<StaffDirectoryFilteringModel, StaffDirectoryFilteringViewModel, IPublishedContent>
    {
        public StaffDirectoryFilteringService(ITagsProvider<IPublishedContent> tagsProvider) : base(tagsProvider)
        {
        }

        public override StaffDirectoryFilteringViewModel GetFilteringViewModel(StaffDirectoryFilteringModel filteringModel, IPublishedContent root)
        {
            var allLocations = _tagsProvider.GetTagsByRoot(root, TagGroups.Locations);
            var allTeams = GetAllTeams(root);

            var viewModel = new StaffDirectoryFilteringViewModel()
            {
                Locations = new FilterGroupViewModel()
                {
                    Title = "Location",
                    Group = FilterNames.Locations,
                    Values = GetFilters(allLocations, filteringModel.Locations)
                },
                Teams = new FilterGroupViewModel()
                {
                    Title = "Team",
                    Group = FilterNames.Teams,
                    Values = GetFilters(allTeams, filteringModel.Teams)
                }
            };

            if (string.IsNullOrWhiteSpace(filteringModel.SearchTerm) == false)
            {
                viewModel.SearchTerm = filteringModel.SearchTerm;
            }

            return viewModel;
        }
    }
}
