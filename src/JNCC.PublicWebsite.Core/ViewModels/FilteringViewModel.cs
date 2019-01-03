namespace JNCC.PublicWebsite.Core.ViewModels
{
    public abstract class FilteringViewModel
    {
        public string SearchTerm { get; set; }
        public FilterGroupViewModel Teams { get; set; }
    }
}