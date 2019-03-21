using JNCC.PublicWebsite.Core.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    public interface IDataHubRawQueryService
    {
        SearchModel GetByRawQuery(string rawQuery, int numberOfItems);
    }
}
