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
        public Guid MemberKey { get; set; }
        public DateTime ExpirationDate { get; set; }
        [NullSetting(NullSetting = NullSettings.Null)]
        public DateTime? ProcessedDate { get; set; }
    }
}
