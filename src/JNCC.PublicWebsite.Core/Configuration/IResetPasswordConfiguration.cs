namespace JNCC.PublicWebsite.Core.Configuration
{
    internal interface IResetPasswordConfiguration
    {
        string FromEmailAddress { get; }
        string EmailTemplatePath { get; }
        int RequestExpirationInMinutes { get; }
    }
}