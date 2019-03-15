using JNCC.PublicWebsite.Core.Models;
using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.Services
{
    public interface IDataHubRawQueryService
    {
        IEnumerable<SearchModel> GetByRawQuery(string query, int numberOfItems);
    }
}
