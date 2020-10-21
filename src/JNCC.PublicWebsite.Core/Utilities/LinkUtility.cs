using System;
using System.Collections.Generic;
using System.Linq;

namespace JNCC.PublicWebsite.Core.Utilities
{
    internal static class LinkUtility
    {
        public static string EnsureHttpsForJnccLinks(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
            {
                if (uri.Host.Contains("jncc.gov.uk"))
                {
                    var b = new UriBuilder(uri)
                    {
                        Scheme = Uri.UriSchemeHttps,
                        Port = -1 // default port for scheme
                    };

                    return b.ToString();
                }
            }

            return url;
        }
    }
}
