using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public class ScienceSliderSchemaViewModel
    {
        public ImageViewModel Image { get; set; }
        public NavigationItemViewModel ImageLink { get; set; }
        public bool ImagePosition { get; set; }
        public string Heading { get; set; }
        public string Text { get; set; }
        public bool ImageTextSection { get; set; }
    }
}
