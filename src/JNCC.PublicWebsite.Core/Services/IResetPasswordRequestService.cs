using JNCC.PublicWebsite.Core.Models;
using System;

namespace JNCC.PublicWebsite.Core.Services
{
    internal interface IResetPasswordRequestService
    {
        ResetPasswordRequestModel Create(Guid memberKey, DateTime currentTime);
        ResetPasswordRequestModel Get(Guid requestToken);
        ResetPasswordRequestModel Get(string requestToken);
        void Update(ResetPasswordRequestModel entry);
    }
}