using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.Providers
{
    public interface ITagsProvider
    {
        IEnumerable<string> GetTags(string tagGroup);
    }

    public interface ITagsProvider<TRoot>
    {
        IEnumerable<string> GetTagsByRoot(TRoot root, string tagGroup);
    }
}
