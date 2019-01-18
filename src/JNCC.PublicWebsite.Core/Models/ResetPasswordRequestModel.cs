using System;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace JNCC.PublicWebsite.Core.Models
{
    [TableName("ResetPasswordRequests")]
    public sealed class ResetPasswordRequestModel
    {
        [PrimaryKeyColumn(AutoIncrement = false)]
        public Guid Id { get; set; }
        public string MemberKey { get; set; }
        public string StatusCD { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
