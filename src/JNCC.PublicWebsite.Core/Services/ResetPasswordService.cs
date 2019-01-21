using JNCC.PublicWebsite.Core.Models;
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
                LogHelper.Error<ResetPasswordService>("Error in TryCreateInitialRequest", ex);

                requestToken = Guid.Empty;
                return false;
            }
        }

        internal bool IsRequestTokenValid(string requestToken)
        {
            var entry = _passwordRequestService.Get(requestToken);

            return IsRequestValid(entry);
        }

        internal bool IsRequestValid(ResetPasswordRequestModel request)
        {
            if (request == null)
            {
                return false;
            }

            if (request.ProcessedDate.HasValue)
            {
                return false;
            }

            return request.ExpirationDate > DateTime.Now;
        }

        internal IMember GetMemberByRequest(ResetPasswordRequestModel request)
        {
            return _memberService.GetByKey(request.MemberKey);
        }

        internal bool ResetPassword(IMember existingMember, ResetPasswordRequestModel request, string newPassword)
        {
            if (existingMember == null)
            {
                return false;
            }

            try
            {
                _memberService.SavePassword(existingMember, newPassword);
                request.ProcessedDate = DateTime.Now;
                _passwordRequestService.Update(request);

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error<ResetPasswordService>("Error in ResetPassword", ex);
                return false;
            }
        }
    }
}
