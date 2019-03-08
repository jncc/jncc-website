using System;
using Umbraco.Web;
using Umbraco.Web.Routing;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class UmbracoContextFullUrlResolverService : IMediaFullUrlResolver, IContentFullUrlResolver
    {
        private readonly string _leftPart;
        private readonly UrlProvider _urlProvider;

        public UmbracoContextFullUrlResolverService(UmbracoContext umbracoContext)
        {
            _urlProvider = umbracoContext.UrlProvider;
            _leftPart = umbracoContext.HttpContext.Request.Url.GetLeftPart(UriPartial.Authority);
        }

        public string ResolveContentFullUrlById(int id)
        {
            return _urlProvider.GetUrl(id, true);
        }

        public string ResolveMediaFullUrl(string url)
        {
            if (url.StartsWith("/"))
            {
                return string.Concat(_leftPart, url);
            }

            return url;
        }
    }
}
