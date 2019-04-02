using System;
using System.Collections.Generic;
using System.Linq;

namespace JNCC.PublicWebsite.Core.Extensions
{
    internal static class StringExtensions
    {
        public static IEnumerable<string> CleanSplit(this string value, char separator)
        {
            return value.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());
        }
    }
}
