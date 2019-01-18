using JNCC.PublicWebsite.Core.Configuration;
using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Models;
using System;
using Umbraco.Core;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class PetaPocoResetPasswordRequestService : IResetPasswordRequestService
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IResetPasswordConfiguration _resetPasswordConfiguration;

        public PetaPocoResetPasswordRequestService(DatabaseContext databaseContext, IResetPasswordConfiguration resetPasswordConfiguration)
        {
            _databaseContext = databaseContext;
            _resetPasswordConfiguration = resetPasswordConfiguration;
        }

        public string Create(object memberKey, DateTime dateTime)
        {
            var expirationDate = dateTime.AddMinutes(_resetPasswordConfiguration.RequestExpirationInMinutes);
            var request = new ResetPasswordRequestModel()
            {
                Id = Guid.NewGuid(),
                MemberKey = memberKey.ToString(),
                StatusCD = ResetPasswordRequestStatusCDs.New,
                ExpirationDate = expirationDate
            };

            _databaseContext.Database.Insert(request);

            return request.Id.ToString();
        }
    }
}