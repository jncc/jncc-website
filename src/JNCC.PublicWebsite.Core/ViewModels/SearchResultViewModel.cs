using System;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class SearchResultViewModel
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string PublishDate { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public string DataType { get; set; }
        public string Site { get; set; }
        public string FileExtension { get; set; }
    }
}
