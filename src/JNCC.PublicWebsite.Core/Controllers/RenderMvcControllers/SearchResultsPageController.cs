using JNCC.PublicWebsite.Core.Configuration;
using JNCC.PublicWebsite.Core.Services;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Security.AntiXss;
using Umbraco.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.RenderMvcControllers
{
    public class SearchResultsPageController : RenderMvcController
    {
        private readonly SearchService _searchService;

        public SearchResultsPageController()
        {
            var config = SearchConfiguration.GetConfig();
            _searchService = new SearchService(config);
        }

        public ActionResult Index(string searchTerm, int currentPage = 1)
        {
            // Cleanese the search term, removing HTML etc
            searchTerm = AntiXssEncoder.HtmlEncode(CleanseSearchTerm(("" + Request["q"]).ToLower(CultureInfo.InvariantCulture)), true);

            // Tokenize the search term
            Tokenize(searchTerm);
            currentPage = int.TryParse(Request["p"], out int parsedInt) ? parsedInt : 1;

            // Call to search ES Index
            var searchResults = _searchService.Query(searchTerm, 10, (currentPage - 1) * 10);

            var viewModel = _searchService.GetViewModel(searchResults, searchTerm, 10, currentPage);

            return CurrentTemplate(viewModel);
        }

        // Cleanse the search term
        public string CleanseSearchTerm(string input)
        {
            return Umbraco.StripHtml(input).ToString();
        }

        // Splits a string on space, except where enclosed in quotes
        public IEnumerable<string> Tokenize(string input)
        {
            return Regex.Matches(input, @"[\""].+?[\""]|[^ ]+")
                .Cast<Match>()
                .Select(m => m.Value.Trim('\"'))
                .ToList();
        }
    }
}
