namespace JNCC.PublicWebsite.Core.Providers
{
    public interface INotificationTemplatesProvider
    {
        string GetTemplateContent(string templateName);
    }
}
