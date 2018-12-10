using System;
using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class LatestNewsItemViewModel
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public IHtmlString Description { get; set; }
        public string Url { get; set; }
    }
}