using JNCC.PublicWebsite.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class SearchViewModel
    {
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<SearchResultViewModel> AllResults { get; set; }
        public IEnumerable<SearchResultViewModel> PagedResults { get; set; }
        public int PageSize { get; set; }
        public string SearchTerm { get; set; }
        public int CurrentPage { get; set; }

    }

    
}
