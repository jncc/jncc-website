using JNCC.PublicWebsite.Core.Configuration;
using JNCC.PublicWebsite.Core.Models;
using System;
using Umbraco.Core;
using Umbraco.Core.Persistence;

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

        public ResetPasswordRequestModel Create(Guid memberKey, DateTime currentDateTime)
        {
            var expirationDate = currentDateTime.AddMinutes(_resetPasswordConfiguration.RequestExpirationInMinutes);
            var request = new ResetPasswordRequestModel()
            {
                Id = Guid.NewGuid(),
                MemberKey = memberKey,
                ExpirationDate = expirationDate
            };

            _databaseContext.Database.Insert(request);

            return request;
        }

        public ResetPasswordRequestModel Get(string requestToken)
        {
            Guid parsedRequestToken;

            if (Guid.TryParse(requestToken, out parsedRequestToken) == false)
            {
                return default(ResetPasswordRequestModel);
            }

            return Get(parsedRequestToken);
        }

        public ResetPasswordRequestModel Get(Guid requestToken)
        {
            var sql = new Sql()
                               .From<ResetPasswordRequestModel>(_databaseContext.SqlSyntax)
                               .Where<ResetPasswordRequestModel>(x => x.Id == requestToken, _databaseContext.SqlSyntax);

            return _databaseContext.Database.FirstOrDefault<ResetPasswordRequestModel>(sql);
        }

        public void Update(ResetPasswordRequestModel entry)
        {
            _databaseContext.Database.Update(entry);
        }
    }
}