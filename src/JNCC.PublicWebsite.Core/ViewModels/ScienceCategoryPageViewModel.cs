using System.Collections.Generic;
using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceCategoryPageViewModel
    {
        public IReadOnlyDictionary<char, IEnumerable<NavigationItemViewModel>> CategorisedPages { get; set; }
    }
}