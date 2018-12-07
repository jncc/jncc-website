using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JNCC.PublicWebsite.Core.Utilities
{
    internal static class ExistenceUtility
    {
        public static bool IsNullOrWhiteSpace(IHtmlString htmlString)
        {
            return htmlString == null || string.IsNullOrWhiteSpace(htmlString.ToHtmlString());
        }
        public static bool IsNullOrEmpty<T>(IEnumerable<T> enumerable)
        {
            return enumerable == null || enumerable.Any() == false;
        }
    }
}
