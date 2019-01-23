using System;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class BlogCommentViewModel
    {
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string Id { get; set; }
    }
}
