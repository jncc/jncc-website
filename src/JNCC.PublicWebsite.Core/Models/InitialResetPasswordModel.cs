﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JNCC.PublicWebsite.Core.Models
{
    public sealed class InitialResetPasswordModel
    {
        [Required]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
    }
}
