using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.Providers
{
    public interface IBlogTagsProvider<TRoot>
    {
        IEnumerable<string> GetAllTagsByRoot(TRoot root);
    }
}
