using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class StaffProfileService
    {
        public StaffProfilePageViewModel GetViewModel(StaffProfilePage content)
        {
            var viewModel = new StaffProfilePageViewModel()
            {
                Name = string.IsNullOrWhiteSpace(content.FullName) ? content.Name : content.FullName,
                JobTitle = content.JobTitle,
                Locations = content.ProfileLocations,
                Teams = content.ProfileTeams,
                TabbedContent = GetTabbedContent(content),
                DirectoryPageUrl = content.Parent.Url
            };

            if (content.ProfilePicture != null)
            {
                viewModel.ImageUrl = content.ProfilePicture.Url;
                viewModel.ImageAltText = content.ProfilePicture.GetPropertyValue<string>("altText").IsNullOrWhiteSpace() ? viewModel.Name : content.ProfilePicture.GetPropertyValue<string>("altText");
                viewModel.ImageTitleText = content.ProfilePicture.GetPropertyValue<string>("titleText").IsNullOrWhiteSpace() ? viewModel.Name : content.ProfilePicture.GetPropertyValue<string>("titleText");
            }

            return viewModel;
        }

        private IReadOnlyDictionary<string, IHtmlString> GetTabbedContent(StaffProfilePage content)
        {
            var tabbedContent = new Dictionary<string, IHtmlString>
            {
                { "Biography", content.BiographyContent },
                { "Projects", content.ProjectsContent },
                { "Publications", content.PublicationsContent },
                { "Research", content.ResearchContent }
            };

            return tabbedContent.Where(x => ExistenceUtility.IsNullOrWhiteSpace(x.Value) == false)
                                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
