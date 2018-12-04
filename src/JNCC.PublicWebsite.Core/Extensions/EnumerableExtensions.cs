using System;
using System.Collections.Generic;
using System.Linq;

namespace JNCC.PublicWebsite.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static IOrderedEnumerable<T> OrderByFirstAvailableProperty<T>(this IEnumerable<T> enumerable, Func<T, IEnumerable<string>> properties)
        {
            return enumerable.OrderBy(x => properties.Invoke(x)
                                                     .Where(y => string.IsNullOrWhiteSpace(y) == false)
                                                     .FirstOrDefault()
                                     );
        }
    }
}
