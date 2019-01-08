using System;
using System.Collections.Generic;
using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ArticlePageViewModel
    {
        public IEnumerable<string> Teams { get; set; }
        public string LandingPageUrl { get; set; }
        public IHtmlString MainContent { get; set; }
        public string ArticleType { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
