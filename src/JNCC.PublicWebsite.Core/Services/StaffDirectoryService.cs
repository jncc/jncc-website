using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Linq;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class StaffDirectoryService : ListingService<StaffDirectoryPage, StaffProfilePage, StaffDirectoryProfileViewModel>
    {
        protected override int GetItemsPerPage(StaffDirectoryPage parent)
        {
            return parent.ProfilesPerPage;
        }

        protected override IOrderedEnumerable<StaffProfilePage> GetOrderedChildren(StaffDirectoryPage parent)
        {
            return parent.Children<StaffProfilePage>()
                         .OrderByFirstAvailableProperty(x => new string[] { x.SortName, x.FullName, x.Name });
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
