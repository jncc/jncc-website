using JNCC.PublicWebsite.Core.Models;
using System.Threading.Tasks;

namespace JNCC.PublicWebsite.Core.Services
{
    internal interface ISearchQueryService
    {
        SearchModel EsGet(string q, int size, int start);

        Task<SearchModel> ESGetAsync(string q, int size, int start);
    }
}