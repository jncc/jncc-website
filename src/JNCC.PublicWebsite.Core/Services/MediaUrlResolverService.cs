namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class MediaUrlResolverService : IMediaFullUrlResolver
    {
        private readonly string _leftPart;

        public MediaUrlResolverService(string leftPart)
        {
            _leftPart = leftPart;
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
