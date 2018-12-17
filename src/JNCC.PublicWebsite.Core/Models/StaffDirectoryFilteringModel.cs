namespace JNCC.PublicWebsite.Core.Models
{
    public sealed class StaffDirectoryFilteringModel : ListFilteringModel
    {
        public string[] Teams { get; set; }
        public string[] Locations { get; set; }
    }
}
