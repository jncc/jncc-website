using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class SearchResultViewModel
    {
        // Page properties
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public string DataType { get; set; }
        public string Site { get; set; }
    }
}
