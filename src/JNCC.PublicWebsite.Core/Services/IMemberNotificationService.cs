using System.Collections.Generic;
using Umbraco.Core.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    internal interface IMemberNotificationService
    {
        void SendNotification(IMember member, IDictionary<string, object> data);
    }
}