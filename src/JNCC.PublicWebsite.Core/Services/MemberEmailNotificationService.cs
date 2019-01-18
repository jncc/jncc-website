using JNCC.PublicWebsite.Core.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using Umbraco.Core.IO;
using Umbraco.Core.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class MemberEmailNotificationService : IMemberNotificationService
    {
        private readonly IResetPasswordConfiguration _resetPasswordConfiguration;
        private readonly IFileSystem _fileSystem;

        public MemberEmailNotificationService(IResetPasswordConfiguration resetPasswordConfiguration)
        {
            _resetPasswordConfiguration = resetPasswordConfiguration;
            _fileSystem = FileSystemProviderManager.Current.GetUnderlyingFileSystemProvider("emailTemplates");
        }

        public void SendNotification(IMember member, IDictionary<string, object> data)
        {
            var message = new MailMessage(_resetPasswordConfiguration.FromEmailAddress, member.Email)
            {
                IsBodyHtml = true,
                Body = BuildTemplate(data)
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Send(message);
            }
        }

        private string BuildTemplate(IDictionary<string, object> data)
        {
            using (var stream = _fileSystem.OpenFile(_resetPasswordConfiguration.EmailTemplatePath))
            {
                using (var reader = new StreamReader(stream))
                {
                    var template = reader.ReadToEnd();

                    foreach (var entry in data)
                    {
                        var templateKey = string.Concat("{{", entry.Key, "}}");

                        template = template.Replace(templateKey, entry.Value.ToString());
                    }

                    return template;
                }
            }
        }
    }
}