using JNCC.PublicWebsite.Core.Models;
using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.Providers
{
    internal interface IScienceDetailsPageProvider
    {
        IEnumerable<ScienceDetailsPage> GetByCategory(ScienceCategoryPage scienceCategoryPage);
    }
}
