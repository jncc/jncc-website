using JNCC.PublicWebsite.Core.Models;
using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.Providers
{
    internal interface ISciencePageCategoriesProvider
    {
        IEnumerable<ScienceCategoryPage> GetCategories(ScienceDetailsPage content);
    }
}