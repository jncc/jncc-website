using System.ComponentModel.DataAnnotations;

namespace JNCC.PublicWebsite.Core.Models
{
    public sealed class LoginFormModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
