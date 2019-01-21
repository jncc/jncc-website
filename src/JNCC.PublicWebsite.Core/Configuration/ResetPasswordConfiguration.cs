using System.Configuration;

namespace JNCC.PublicWebsite.Core.Configuration
{
    public sealed class ResetPasswordConfiguration : ConfigurationSection, IResetPasswordConfiguration
    {
        [ConfigurationProperty("FromEmailAddress")]
        public string FromEmailAddress { get { return (string)this["FromEmailAddress"]; } }

        [ConfigurationProperty("InitialRequestEmailTemplatePath")]
        public string InitialRequestEmailTemplatePath { get { return (string)this["InitialRequestEmailTemplatePath"]; } }

        [ConfigurationProperty("RequestExpirationInMinutes")]
        public int RequestExpirationInMinutes { get { return (int)this["RequestExpirationInMinutes"]; } }

        [ConfigurationProperty("CompletedRequestEmailTemplatePath")]
        public string CompletedRequestEmailTemplatePath { get { return (string)this["CompletedRequestEmailTemplatePath"]; } }

        internal static ResetPasswordConfiguration GetConfig()
        {
            return ConfigurationManager.GetSection("resetPasswordConfig") as ResetPasswordConfiguration;
        }
    }
}
