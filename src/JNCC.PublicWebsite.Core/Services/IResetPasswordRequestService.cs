using System;

namespace JNCC.PublicWebsite.Core.Services
{
    internal interface IResetPasswordRequestService
    {
        string Create(object memberKey, DateTime expiration);
    }
}