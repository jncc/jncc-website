namespace JNCC.PublicWebsite.Core.Models
{
    public class FilteringModel
    {
        public FilteringModel()
        {
            PageNumber = 1;
        }

        public int PageNumber { get; set; }
    }

    internal interface ITeamsFiltering
    {
        string[] Teams { get; set; }
    }
    internal interface ISearchTermFiltering
    {
        string SearchTerm { get; set; }
    }
}