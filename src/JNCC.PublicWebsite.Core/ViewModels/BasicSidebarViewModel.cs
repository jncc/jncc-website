using JNCC.PublicWebsite.Core.Utilities;
using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public abstract class BasicSidebarViewModel
    {
        public NavigationItemViewModel PrimaryCallToActionButton { get; set; }
        public bool HasPrimaryCallToActionButton
        {
            get
            {
                return PrimaryCallToActionButton != null;
            }
        }

        public IEnumerable<NavigationItemViewModel> DataHubLinks { get; set; }
        public bool HasDataHubLinks
        {
            get
            {
                return ExistenceUtility.IsNullOrEmpty(DataHubLinks) == false;
            }
        }

        public IEnumerable<NavigationItemViewModel> SeeAlsoLinks { get; set; }
        public bool HasSeeAlsoLinks
        {
            get
            {
                return ExistenceUtility.IsNullOrEmpty(SeeAlsoLinks) == false;
            }
        }
    }
}