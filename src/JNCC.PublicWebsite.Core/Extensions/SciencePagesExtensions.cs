using JNCC.PublicWebsite.Core.Models;
using System.Linq;

namespace JNCC.PublicWebsite.Core.Extensions
{
    internal static class SciencePagesExtensions
    {
        public static char GetCategorisationCharacter<T>(this T model) where T : ISciencePageCategorisationComposition, IPageHeroComposition
        {
            var names = new string[]
            {
                model.CategoryOrderingName,
                model.Headline,
                model.Name
            };

            var firstAvailableName = names.FirstOrDefault(x => string.IsNullOrWhiteSpace(x) == false);

            return firstAvailableName.ToUpper().First();
        }
    }
}
