using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Services;

namespace JNCC.PublicWebsite.Core.Providers
{
    internal sealed class UmbracoContentTagsProvider : ITagsProvider
    {
        private readonly ITagService _tagService;

        public UmbracoContentTagsProvider(ITagService tagService)
        {
            _tagService = tagService;
        }

        public IEnumerable<string> GetTags(string tagGroup)
        {
            return _tagService.GetAllContentTags(tagGroup).OrderBy(x => x.Text)
                                                          .Select(x => x.Text);
        }
    }
}
