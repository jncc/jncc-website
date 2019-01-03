using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Umbraco.Core;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class StaffDirectoryService : ListingService<StaffDirectoryPage, StaffProfilePage, StaffDirectoryProfileViewModel, StaffDirectoryFilteringModel>
    {
        public override NameValueCollection ConvertFiltersToNameValueCollection(StaffDirectoryFilteringModel filteringModel)
        {
            var collection = new NameValueCollection();

            if (ExistenceUtility.IsNullOrEmpty(filteringModel.Locations) == false)
            {
                foreach (var value in filteringModel.Locations)
                {
                    collection.Add(FilterNames.Locations, value);
                }
            }

            if (ExistenceUtility.IsNullOrEmpty(filteringModel.Teams) == false)
            {
                foreach (var value in filteringModel.Teams)
                {
                    collection.Add(FilterNames.Teams, value);
                }
            }

            if (string.IsNullOrWhiteSpace(filteringModel.SearchTerm) == false)
            {
                collection.Add(FilterNames.SearchTerm, filteringModel.SearchTerm);
            }

            return collection;
        }

        protected override int GetItemsPerPage(StaffDirectoryPage parent)
        {
            return parent.ProfilesPerPage;
        }

        protected override IOrderedEnumerable<StaffProfilePage> GetOrderedChildren(StaffDirectoryPage parent, StaffDirectoryFilteringModel filteringModel)
        {
            var allChildren = parent.Children<StaffProfilePage>();
            var conditions = new List<Func<StaffProfilePage, bool>>();

            if (ExistenceUtility.IsNullOrEmpty(filteringModel.Teams) == false)
            {
                conditions.Add(x => filteringModel.Teams.Any(y => x.ProfileTeams.Contains(y, StringComparer.OrdinalIgnoreCase)));
            }

            if (ExistenceUtility.IsNullOrEmpty(filteringModel.Locations) == false)
            {
                conditions.Add(x => filteringModel.Locations.Any(y => x.ProfileLocations.Contains(y, StringComparer.OrdinalIgnoreCase)));
            }

            if (string.IsNullOrEmpty(filteringModel.SearchTerm) == false)
            {
                conditions.Add(x => x.Name.InvariantContains(filteringModel.SearchTerm)
                                 || x.FullName.InvariantContains(filteringModel.SearchTerm)
                                 || x.JobTitle.InvariantContains(filteringModel.SearchTerm)
                                 || (ExistenceUtility.IsNullOrWhiteSpace(x.DirectoryListingContent) == false && x.DirectoryListingContent.ToString().InvariantContains(filteringModel.SearchTerm))
                              );
            }

            var actualChildren = ExistenceUtility.IsNullOrEmpty(conditions) ? allChildren : allChildren.Where(x => conditions.All(y => y.Invoke(x)));

            return actualChildren.OrderByFirstAvailableProperty(x => new string[] { x.SortName, x.FullName, x.Name });
        }

        protected override StaffDirectoryProfileViewModel ToViewModel(StaffProfilePage content)
        {
            var viewModel = new StaffDirectoryProfileViewModel
            {
                Name = string.IsNullOrWhiteSpace(content.FullName) ? content.Name : content.FullName,
                JobTitle = content.JobTitle,
                Description = content.DirectoryListingContent,
                Url = content.Url
            };

            if (content.ProfilePicture != null)
            {
                viewModel.ImageUrl = content.ProfilePicture.GetCropUrl(ImageCropAliases.ListingThumbnail);
            }

            return viewModel;
        }
    }
}
