using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using Umbraco.Core.Logging;

namespace JNCC.PublicWebsite.Core.HttpModules
{
    public sealed class CsvRedirectsHttpModule : RedirectsHttpModuleBase
    {
        private readonly IConfigurationProvider _configurationProvider;

        public CsvRedirectsHttpModule()
        {
            _configurationProvider = new AppSettingsConfigurationProvider();
        }

        protected override IDictionary<string, string> GetRedirectDictionary()
        {
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var filePath = GetFilePath();

            if (string.IsNullOrWhiteSpace(filePath))
            {
                LogHelper.Warn<CsvRedirectsHttpModule>($"Skipping Redirection. No file path configured.");
                return dict;
            }

            if (File.Exists(filePath) == false)
            {
                LogHelper.Warn<CsvRedirectsHttpModule>($"Skipping Redirection. Unable to find file {filePath}.");
                return dict;
            }

            var lineNumber = 0;
            using (var fs = File.OpenRead(filePath))
            {
                using (var reader = new StreamReader(fs))
                {
                    var currentDomain = CurrentDomainWithoutProtocol();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (!string.IsNullOrEmpty(line) && !line.StartsWith("//"))
                        {
                            var values = line.CleanSplit(',');

                            if (values.Count() >= 2)
                            {
                                var key = StripHttpProtocol(values.ElementAt(0));
                                var value = values.ElementAt(1);

                                dict.Add(PrepareUrlForDictionary(key, currentDomain), value);
                            }
                            else
                            {
                                LogHelper.Warn<CsvRedirectsHttpModule>($"Skipping line {lineNumber}. Insufficient data.");
                            }
                        }
                        lineNumber++;
                    }
                }
            }

            LogHelper.Info<CsvRedirectsHttpModule>($"{dict.Count()} redirection rules found over {lineNumber} lines.");

            return dict;
        }

        private string GetFilePath()
        {
            var filePath = _configurationProvider.GetValue<string>("RedirectsHttpModule:FilePath");

            if (string.IsNullOrEmpty(filePath))
            {
                return null;
            }

            if (filePath.StartsWith("~") || filePath.StartsWith("/"))
            {
                return HostingEnvironment.MapPath(filePath);
            }

            return filePath;
        }

        protected override bool IsEnabled()
        {
            return _configurationProvider.GetValue<bool>("RedirectsHttpModule:Enabled");
        }

        private string PrepareUrlForDictionary(string url, string currentDomain)
        {
            if (url.StartsWith("/") == false)
            {
                return url;
            }
            return ProcessUrl(url, currentDomain);
        }
    }
}
