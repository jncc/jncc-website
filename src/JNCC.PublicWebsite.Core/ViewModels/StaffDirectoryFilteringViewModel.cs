using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class StaffDirectoryFilteringViewModel : FilteringViewModel
    {
        public IReadOnlyDictionary<string, bool> Locations { get; set; }
        public IReadOnlyDictionary<string, bool> Teams { get; set; }
    }
}
