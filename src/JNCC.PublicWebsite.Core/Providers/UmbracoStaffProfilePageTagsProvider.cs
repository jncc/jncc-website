using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Models;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Cache;
using Umbraco.Core.Models;

namespace JNCC.PublicWebsite.Core.Providers
{
    internal sealed class UmbracoStaffProfilePageTagsProvider : UmbracoPagesProvider<StaffProfilePage>, ITagsProvider<IPublishedContent>
    {
        public UmbracoStaffProfilePageTagsProvider(ICacheProvider cacheProvider) : base(cacheProvider)
        {
        }

        public IEnumerable<string> GetTagsByRoot(IPublishedContent root, string tagGroup)
        {
            var profilePages = GetContentPages(root);

            switch (tagGroup)
            {
                case TagGroups.Locations:
                    return profilePages.SelectMany(x => x.ProfileLocations)
                                       .Distinct()
                                       .OrderBy(x => x);
                case TagGroups.Teams:
                    return profilePages.SelectMany(x => x.ProfileTeams)
                                       .Distinct()
                                       .OrderBy(x => x);
                default:
                    return Enumerable.Empty<string>();
            }
        }
    }
}
