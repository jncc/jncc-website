using JNCC.PublicWebsite.Core.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using Umbraco.Core.IO;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class ResetPasswordEmailNotificationService
    {
        private readonly IResetPasswordConfiguration _resetPasswordConfiguration;
        private readonly IFileSystem _fileSystem;

        public ResetPasswordEmailNotificationService(IResetPasswordConfiguration resetPasswordConfiguration)
        {
            _resetPasswordConfiguration = resetPasswordConfiguration;
            _fileSystem = FileSystemProviderManager.Current.GetUnderlyingFileSystemProvider("emailTemplates");
        }

        public void SendInitialRequestEmail(IMember member, Guid token, IPublishedContent currentPage)
        {
            var data = new Dictionary<string, object>()
            {
                { "Token", token },
                { "ResetPasswordPageUrl", currentPage.UrlWithDomain() }
            };

            SendNotification(member, data, _resetPasswordConfiguration.EmailTemplatePath);
        }

        private void SendNotification(IMember member, IDictionary<string, object> data, string templateName)
        {
            var templateBody = GetTemplateContent(templateName);
            var message = new MailMessage(_resetPasswordConfiguration.FromEmailAddress, member.Email)
            {
                IsBodyHtml = true,
                Body = BuildTemplate(templateBody, data)
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Send(message);
            }
        }

        private string GetTemplateContent(string templateName)
        {
            using (var stream = _fileSystem.OpenFile(templateName))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private string BuildTemplate(string templateBody, IDictionary<string, object> data)
        {
            var modifiedTemplateBody = templateBody;
            foreach (var entry in data)
            {
                var templateKey = string.Concat("{{", entry.Key, "}}");

                modifiedTemplateBody = modifiedTemplateBody.Replace(templateKey, entry.Value.ToString());
            }

            return modifiedTemplateBody;
        }
    }
}
