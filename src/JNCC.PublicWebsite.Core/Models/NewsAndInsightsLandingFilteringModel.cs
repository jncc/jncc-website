namespace JNCC.PublicWebsite.Core.Models
{
    public sealed class NewsAndInsightsLandingFilteringModel : FilteringModel, ISearchTermFiltering, ITeamsFiltering
    {
        public string SearchTerm { get; set; }
        public string[] Teams { get; set; }
        public string[] ArticleTypes { get; set; }
        public int[] Years { get; set; }
    }
}
