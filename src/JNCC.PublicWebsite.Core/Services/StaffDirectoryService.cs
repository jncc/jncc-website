using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Core.Persistence;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Services
{
    public sealed class StaffDirectoryService
    {
        public Paged​Result<StaffDirectoryProfileViewModel> GetViewModels(StaffDirectoryPage staffDirectoryPage, int pageNumber)
        {
            var profilesPerPage = decimal.ToInt32(staffDirectoryPage.ProfilesPerPage);

            var children = staffDirectoryPage.Children<StaffProfilePage>();
            var results = new Paged​Result<StaffDirectoryProfileViewModel>(children.LongCount(), pageNumber, profilesPerPage);
            var viewModels = children.Skip(results.GetSkipSize()).Take(profilesPerPage).Select(ToViewModel);
            results.Items = viewModels;

            return results;
        }

        private StaffDirectoryProfileViewModel ToViewModel(StaffProfilePage content)
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
