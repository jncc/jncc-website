namespace JNCC.PublicWebsite.Core.Configuration
{
    internal interface IResetPasswordConfiguration
    {
        string FromEmailAddress { get; }
        string InitialRequestEmailTemplatePath { get; }
        string CompletedRequestEmailTemplatePath { get; }
        int RequestExpirationInMinutes { get; }
    }
}