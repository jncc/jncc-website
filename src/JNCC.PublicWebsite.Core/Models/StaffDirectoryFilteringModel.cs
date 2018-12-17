namespace JNCC.PublicWebsite.Core.Models
{
    public sealed class StaffDirectoryFilteringModel : FilteringModel
    {
        public string[] Teams { get; set; }
        public string[] Locations { get; set; }
    }
}
