using JNCC.PublicWebsite.Core.Models;
using System.Threading.Tasks;

namespace JNCC.PublicWebsite.Core.Services
{
    internal interface ISearchQueryService
    {
        SearchModel Query(string query, int size, int start, string siteFilter);

        Task<SearchModel> QueryAsync(string query, int size, int start, string siteFilter);
    }
}