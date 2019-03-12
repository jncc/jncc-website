using System;
using System.Configuration;
using Umbraco.Core.Logging;

namespace JNCC.PublicWebsite.Core.Providers
{
    internal class AppSettingsConfigurationProvider : IConfigurationProvider
    {
        public T GetValue<T>(string key)
        {
            return GetValue(key, default(T));
        }

        public T GetValue<T>(string key, T fallbackValue)
        {
            var configValue = GetConfigValue(key);

            if (string.IsNullOrWhiteSpace(configValue))
            {
                return fallbackValue;
            }

            T value;

            try
            {
                value = (T)Convert.ChangeType(configValue, typeof(T));
            }
            catch (Exception ex)
            {
                var message = string.Format("Invalid value \"{0}\" for key \"{1}\" when cast as a {2}.", configValue, key, typeof(T));
                LogHelper.Error<AppSettingsConfigurationProvider>(message, ex);
                return fallbackValue;
            }

            return value;
        }

        private string GetConfigValue(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }
    }
}
