using System.IO;
using Umbraco.Core.IO;

namespace JNCC.PublicWebsite.Core.Providers
{
    internal sealed class UmbracoFileSystemEmailTemplatesProvider : INotificationTemplatesProvider
    {
        private readonly IFileSystem _fileSystem;

        public UmbracoFileSystemEmailTemplatesProvider()
        {
            _fileSystem = FileSystemProviderManager.Current.GetUnderlyingFileSystemProvider("emailTemplates");
        }

        public string GetTemplateContent(string templateName)
        {
            using (var stream = _fileSystem.OpenFile(templateName))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
