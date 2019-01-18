using System;
using System.Configuration;

namespace JNCC.PublicWebsite.Core.Configuration
{
    public sealed class ResetPasswordConfiguration : ConfigurationSection, IResetPasswordConfiguration
    {
        [ConfigurationProperty("FromEmailAddress")]
        public string FromEmailAddress { get { return (string)this["FromEmailAddress"]; } }

        [ConfigurationProperty("EmailTemplatePath")]
        public string EmailTemplatePath { get { return (string)this["EmailTemplatePath"]; } }

        [ConfigurationProperty("RequestExpirationInMinutes")]
        public int RequestExpirationInMinutes { get { return (int)this["RequestExpirationInMinutes"]; } }

        internal static ResetPasswordConfiguration GetConfig()
        {
            return ConfigurationManager.GetSection("resetPasswordConfig") as ResetPasswordConfiguration;
        }
    }
}
