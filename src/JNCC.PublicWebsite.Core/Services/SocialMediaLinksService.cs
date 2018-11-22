using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using Umbraco.Web.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    public sealed class SocialMediaLinksService
    {
        public IEnumerable<SocialMediaNavigationItemViewModel> GetSocialMediaLinks(RelatedLinks relatedLinks)
        {
            var links = new List<SocialMediaNavigationItemViewModel>();

            if (relatedLinks == null)
            {
                return links;
            }

            foreach (var relatedLink in relatedLinks)
            {
                var text = relatedLink.Caption;
                var link = new SocialMediaNavigationItemViewModel
                {
                    Url = relatedLink.Link,
                    Text = text,
                    IconClassSuffix = text.ToLower(),
                    Target = relatedLink.NewWindow ? HtmlAnchorTargets.Blank : null
                };

                links.Add(link);
            }

            return links;
        }
    }
}
