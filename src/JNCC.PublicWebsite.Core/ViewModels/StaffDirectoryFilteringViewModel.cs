using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class StaffDirectoryFilteringViewModel : FilteringViewModel
    {
        public FilterGroupViewModel Locations { get; set; }
        public FilterGroupViewModel Teams { get; set; }
    }
}
