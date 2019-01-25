using JNCC.PublicWebsite.Core.Configuration;
using JNCC.PublicWebsite.Core.Providers;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class ResetPasswordEmailNotificationService
    {
        private readonly IResetPasswordConfiguration _resetPasswordConfiguration;
        private readonly INotificationTemplatesProvider _templatesProvider;

        public ResetPasswordEmailNotificationService(IResetPasswordConfiguration resetPasswordConfiguration)
        {
            _resetPasswordConfiguration = resetPasswordConfiguration;
            _templatesProvider = new UmbracoFileSystemEmailTemplatesProvider();
        }

        private void SendNotification(IMember member, string subject, string templateName, IDictionary<string, object> data = null)
        {
            var templateContent = _templatesProvider.GetTemplateContent(templateName);
            var templateBody = data == null ? templateContent : BuildTemplate(templateContent, data);

            var message = new MailMessage(_resetPasswordConfiguration.FromEmailAddress, member.Email)
            {
                Subject = subject,
                IsBodyHtml = true,
                Body = templateBody
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Send(message);
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

        internal void SendCompletedRequestEmail(IMember member, IPublishedContent currentPage)
        {
            var data = new Dictionary<string, object>()
            {
                { "ResetPasswordPageUrl", currentPage.UrlWithDomain() }
            };

            SendNotification(member, "Reset Password Complete", _resetPasswordConfiguration.CompletedRequestEmailTemplatePath, data);
        }

        internal void SendInitialRequestEmail(IMember member, Guid token, IPublishedContent currentPage)
        {
            var data = new Dictionary<string, object>()
            {
                { "Token", token },
                { "ResetPasswordPageUrl", currentPage.UrlWithDomain() }
            };

            SendNotification(member, "Reset Password Request", _resetPasswordConfiguration.InitialRequestEmailTemplatePath, data);
        }
    }
}
