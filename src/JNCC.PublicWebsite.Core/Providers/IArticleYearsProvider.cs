using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.Providers
{
    public interface IArticleYearsProvider
    {
        IEnumerable<int> GetAll();
        IEnumerable<int> GetAllDescending();
    }
}
