using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace JNCC.PublicWebsite.Core.Models
{
    public sealed class ChangePasswordModel
    {
        [Required]
        [DisplayName("Old Password")]
        public string OldPassword { get; set; }
        [Required]
        [MembershipPassword]
        [DisplayName("New Password")]
        public string NewPassword { get; set; }
        [Required]
        [Compare(nameof(NewPassword))]
        [DisplayName("Confirm New Password")]
        public string ConfirmNewPassword { get; set; }
    }
}
