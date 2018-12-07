using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using System.Web;

namespace JNCC.PublicWebsite.Core.Services
{
    public sealed class StaffProfileService
    {
        public StaffProfilePageViewModel GetViewModel(StaffProfilePage content)
        {
            var viewModel = new StaffProfilePageViewModel()
            {
                Name = string.IsNullOrWhiteSpace(content.FullName) ? content.Name : content.FullName,
                JobTitle = content.JobTitle,
                ProfileTags = content.ProfileTags,
                TabbedContent = GetTabbedContent(content)
            };

            if (content.ProfilePicture != null)
            {
                viewModel.ImageUrl = content.ProfilePicture.Url;
            }

            return viewModel;
        }

        private IReadOnlyDictionary<string, IHtmlString> GetTabbedContent(StaffProfilePage content)
        {
            var tabbedContent = new Dictionary<string, IHtmlString>
            {
                { "Biography", content.BiographyContent }
            };

            if (ExistenceUtility.IsNullOrWhiteSpace(content.ProjectsContent) == false)
            {
                tabbedContent.Add("Projects", content.ProjectsContent);
            }

            if (ExistenceUtility.IsNullOrWhiteSpace(content.PublicationsContent) == false)
            {
                tabbedContent.Add("Publications", content.PublicationsContent);
            }

            if (ExistenceUtility.IsNullOrWhiteSpace(content.ResearchContent) == false)
            {
                tabbedContent.Add("Research", content.ResearchContent);
            }

            return tabbedContent;
        }
    }
}
