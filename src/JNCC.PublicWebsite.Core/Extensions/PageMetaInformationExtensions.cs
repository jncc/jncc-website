using JNCC.PublicWebsite.Core.Models;
using System;

namespace JNCC.PublicWebsite.Core.Extensions
{
    internal static class PageMetaInformationExtensions
    {
        public static DateTime GetPublishedDateOrDefault(this IPageMetaInformationComposition composition)
        {
            if (composition.PublishedDate == default(DateTime))
            {
                return composition.UpdateDate;
            }

            return composition.PublishedDate;
        }
    }
}
