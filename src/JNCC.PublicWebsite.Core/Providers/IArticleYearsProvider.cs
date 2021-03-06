﻿using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.Providers
{
    public interface IArticleYearsProvider
    {
        IEnumerable<int> GetAll();
        IEnumerable<int> GetAllDescending();

    }

    public interface IArticleYearsProvider<TRoot>
    {
        IEnumerable<int> GetAllByRoot(TRoot root);
        IEnumerable<int> GetAllByRootDescending(TRoot root);
    }
}
