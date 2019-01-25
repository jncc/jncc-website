namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class MemberLoggedInBarViewModel
    {
        public string MemberAccountPageUrl { get; set; }
        public bool HasMemberAccountPageUrl
        {
            get
            {
                return string.IsNullOrWhiteSpace(MemberAccountPageUrl) == false;
            }
        }
    }
}
