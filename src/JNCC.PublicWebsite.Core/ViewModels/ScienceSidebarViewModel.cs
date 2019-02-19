using JNCC.PublicWebsite.Core.Utilities;
using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceSidebarViewModel : BasicSidebarViewModel
    {
        public IEnumerable<MainNavigationItemViewModel> Categories { get; set; }

        public bool HasCategories
        {
            get
            {
                return ExistenceUtility.IsNullOrEmpty(Categories) == false;
            }
        }
    }
}