using System.Collections.Generic;
using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class StaffProfilePageViewModel
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string JobTitle { get; set; }
        public IEnumerable<string> ProfileTags { get; set; }
        public IReadOnlyDictionary<string, IHtmlString> TabbedContent { get; set; }
    }
}
