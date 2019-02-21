namespace JNCC.PublicWebsite.Core.Models
{
    public sealed class StaffDirectoryFilteringModel : FilteringModel, ISearchTermFiltering, ITeamsFiltering
    {
        public string SearchTerm { get; set; }
        public string[] Teams { get; set; }
        public string[] Locations { get; set; }
    }
}
