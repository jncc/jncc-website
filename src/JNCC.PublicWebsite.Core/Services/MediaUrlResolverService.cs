namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class MediaUrlResolverService
    {
        private readonly string _leftPart;

        public MediaUrlResolverService(string leftPart)
        {
            _leftPart = leftPart;
        }

        public string ResolveUrl(string url)
        {
            if (url.StartsWith("/"))
            {
                return string.Concat(_leftPart, url);
            }

            return url;
        }
    }
}
