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
    }
}