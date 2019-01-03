namespace JNCC.PublicWebsite.Core.Models
{
    public class FilteringModel
    {
        public FilteringModel()
        {
            PageNumber = 1;
        }

        public string SearchTerm { get; set; }
        public int PageNumber { get; set; }
        public string[] Teams { get; set; }
    }
}