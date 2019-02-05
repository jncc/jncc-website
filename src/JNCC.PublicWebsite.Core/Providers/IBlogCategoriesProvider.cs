using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.Providers
{
    public interface IBlogCategoriesProvider<TRoot>
    {
        IEnumerable<string> GetAllCategoriesByRoot(TRoot root);
    }
}
