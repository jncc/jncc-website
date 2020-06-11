using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class StaffDirectoryProfileViewModel
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string ImageAltText { get; set; }
        public string ImageTitleText { get; set; }
        public string JobTitle { get; set; }
        public IHtmlString Description { get; set; }
        public string Url { get; set; }
    }
}
