using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using Umbraco.Web.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class SocialMediaLinksService
    {
        private readonly NavigationItemService _navigationItemService;

        public SocialMediaLinksService(NavigationItemService navigationItemService)
        {
            _navigationItemService = navigationItemService;
        }

        public IEnumerable<SocialMediaNavigationItemViewModel> GetSocialMediaLinks(RelatedLinks relatedLinks)
        {
            var links = new List<SocialMediaNavigationItemViewModel>();

            if (relatedLinks == null)
            {
                return links;
            }

            foreach (var relatedLink in relatedLinks)
            {
                var link = _navigationItemService.GetViewModel<SocialMediaNavigationItemViewModel>(relatedLink);
                link.IconClassSuffix = relatedLink.Caption.ToLower();

                links.Add(link);
            }

            return links;
        }
    }
}
