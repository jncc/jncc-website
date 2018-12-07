using System.Collections.Generic;
using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class CarouselViewModel
    {
        public string Headline { get; set; }
        public IHtmlString Text { get; set; }
        public IEnumerable<string> ImageUrls { get; set; }
    }
}