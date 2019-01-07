using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.Providers
{
    public interface IArticleTypesProvider
    {
        IEnumerable<string> GetAll();
    }

    public interface IArticleTypesProvider<TRoot>
    {
        IEnumerable<string> GetAllByRoot(TRoot root);
    }
}
