using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Extensions
{
    public static class PublishedContentExtensions
    {
        public static bool AreChildrenVisible(this IPublishedContent content)
        {
            return content.GetPropertyValue<bool>("umbracoNaviHideChildren") == false;
        }

        public static T Site<T>(this IPublishedContent content) where T : class, IPublishedContent
        {
            var site = content.Site();

            return site as T;
        }

        public static IEnumerable<IPublishedContent> VisibleChildren(this IPublishedContent content)
        {
            return content.Children.Where(x => x.IsVisible());
        }
    }
}
