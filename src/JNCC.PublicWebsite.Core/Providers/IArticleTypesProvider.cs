using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.Providers
{
    public interface IArticleTypesProvider
    {
        IEnumerable<string> GetAll();
    }
}
