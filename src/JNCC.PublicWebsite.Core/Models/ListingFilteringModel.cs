namespace JNCC.PublicWebsite.Core.Models
{
    public class ListFilteringModel
    {
        public ListFilteringModel()
        {
            PageNumber = 1;
        }

        public string SearchTerm { get; set; }
        public int PageNumber { get; set; }
    }
}