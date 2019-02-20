using JNCC.PublicWebsite.Core.Models;
using System.Linq;

namespace JNCC.PublicWebsite.Core.Extensions
{
    internal static class PageHeroExtensions
    {
        public static string GetHeadline(this IPageHeroComposition content)
        {
            var names = new string[]
            {
                content.Headline,
                content.Name
            };

            return names.FirstOrDefault(x => string.IsNullOrWhiteSpace(x) == false);
        }
    }
}
