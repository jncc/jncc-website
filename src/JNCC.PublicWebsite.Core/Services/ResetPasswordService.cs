using System;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class ResetPasswordService
    {
        private readonly IMemberService _memberService;
        private readonly IResetPasswordRequestService _passwordRequestService;

        public ResetPasswordService(IMemberService memberService, IResetPasswordRequestService passwordRequestService)
        {
            _memberService = memberService;
            _passwordRequestService = passwordRequestService;
        }

        internal bool TryCreateInitialRequest(IMember existingMember, out Guid requestToken)
        {
            try
            {
                var request = _passwordRequestService.Create(existingMember.Key, DateTime.Now);

                requestToken = request.Id;
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error<ResetPasswordService>("Error in TrySetInitialRequest", ex);

                requestToken = Guid.Empty;
                return false;
            }
        }

        internal bool IsRequestTokenValid(string requestToken)
        {
            Guid parsedRequestToken;

            if (Guid.TryParse(requestToken, out parsedRequestToken) == false)
            {
                return false;
            }

            var entry = _passwordRequestService.Get(parsedRequestToken);

            if (entry == null)
            {
                return false;
            }

            if (entry.ProcessedDate.HasValue)
            {
                return false;
            }

            return entry.ExpirationDate > DateTime.Now;
        }
    }
}
