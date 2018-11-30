using Umbraco.Core.Models;

namespace JNCC.PublicWebsite.Core.Extensions
{
    public static class PagedResultExtensionMethods
    {
        public static bool IsPageNumberGreaterThanFirst<T>(this PagedResult<T> pagedResult)
        {
            return pagedResult.PageNumber > 1;
        }
        public static bool IsPageNumberLessThanLast<T>(this PagedResult<T> pagedResult)
        {
            return pagedResult.PageNumber < pagedResult.TotalPages;
        }

        public static long PreviousPageNumber<T>(this PagedResult<T> pagedResult)
        {
            return pagedResult.PageNumber - 1;
        }
        public static long NextPageNumber<T>(this PagedResult<T> pagedResult)
        {
            return pagedResult.PageNumber + 1;
        }
    }
}
