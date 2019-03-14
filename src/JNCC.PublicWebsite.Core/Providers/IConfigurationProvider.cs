namespace JNCC.PublicWebsite.Core.Providers
{
    public interface IConfigurationProvider
    {
        T GetValue<T>(string key);
        T GetValue<T>(string key, T fallbackValue);
    }
}
