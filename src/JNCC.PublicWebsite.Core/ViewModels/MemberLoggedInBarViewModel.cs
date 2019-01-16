namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class MemberLoggedInBarViewModel
    {
        public string ChangePasswordPageUrl { get; set; }
        public bool HasChangePasswordPageUrl
        {
            get
            {
                return string.IsNullOrWhiteSpace(ChangePasswordPageUrl) == false;
            }
        }
    }
}
