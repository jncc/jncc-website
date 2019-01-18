using System;
using System.Collections.Generic;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class ResetPasswordService
    {
        private readonly IResetPasswordRequestService _passwordRequestService;
        private readonly IMemberNotificationService _memberNotificationService;
        public ResetPasswordService(IMemberService memberService, IResetPasswordRequestService passwordRequestService, IMemberNotificationService memberNotificationService)
        {
            _passwordRequestService = passwordRequestService;
            _memberNotificationService = memberNotificationService;
        }

        internal bool TrySetInitialRequest(IMember existingMember, IPublishedContent currentPage)
        {
            try
            {
                var token = _passwordRequestService.Create(existingMember.Key, DateTime.Now);
                var data = new Dictionary<string, object>()
                {
                    { "Token", token },
                    { "ResetPasswordPageUrl", currentPage.UrlWithDomain() }
                };

                _memberNotificationService.SendNotification(existingMember, data);

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error<ResetPasswordService>("Error in TrySetInitialRequest", ex);
                return false;
            }
        }
    }
}
