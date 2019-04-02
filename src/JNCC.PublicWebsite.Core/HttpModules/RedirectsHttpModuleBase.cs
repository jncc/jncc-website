using System;
using System.Collections.Generic;
using System.Web;
using Umbraco.Core;

namespace JNCC.PublicWebsite.Core.HttpModules
{
    /// <summary>
    /// A dead simple CSV 301 redirect module
    /// </summary>
    public abstract class RedirectsHttpModuleBase : IHttpModule
    {
        // Current Request
        public HttpRequest Request => HttpContext.Current.Request;

        /// <summary>
        /// Current Response
        /// </summary>
        public HttpResponse Response => HttpContext.Current.Response;

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            context.EndRequest += ContextOnEndRequest;
        }

        // <summary>
        // Mange the end request and redirect
        // </summary>
        // <param name="sender"></param>
        // <param name="eventArgs"></param>
        private void ContextOnEndRequest(object sender, EventArgs eventArgs)
        {
            var application = (HttpApplication)sender;

            // Look for a redirect matching the URL
            var processedUrl = ProcessUrl(Request.RawUrl, CurrentDomainWithoutProtocol());

            // All the redirects
            var allRedirects = Redirects();

            // Get a querystring less url (If it has one)
            if (processedUrl.Contains("?"))
            {
                // We get the url without the querystring, then add a * as that's the wildcard match
                var querystringLessUrl = string.Concat(processedUrl.Split('?')[0], "*");

                // See if we have a wildcard url
                if (allRedirects.ContainsKey(querystringLessUrl))
                {
                    Response.RedirectPermanent(allRedirects[querystringLessUrl]);
                }
            }

            // Default - Check if it's in the dictionary            
            if (allRedirects.ContainsKey(processedUrl))
            {
                Response.RedirectPermanent(allRedirects[processedUrl]);
            }
        }

        /// <summary>
        /// Grabs all the redirects from the csv and dumps them in a dictionary and holds in runtime cache
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> Redirects()
        {
            return (Dictionary<string, string>)ApplicationContext.Current.ApplicationCache.RuntimeCache.GetCacheItem("allpageredirects", () =>
            {
                return GetRedirectDictionary();
            });
        }

        protected abstract IDictionary<string, string> GetRedirectDictionary();

        /// <summary>
        /// Any pre url processing happens here
        /// </summary>
        /// <param name="rawUrl"></param>
        /// <param name="currentDomain"></param>
        /// <returns></returns>
        protected static string ProcessUrl(string rawUrl, string currentDomain)
        {
            // Get the current domain
            return string.Concat(currentDomain, rawUrl).Trim();
        }

        /// <summary>
        /// Gets the current domain with the Protocol
        /// </summary>
        /// <returns></returns>
        protected string CurrentDomainWithProtocol()
        {
            return CurrentDomain(true);
        }

        /// <summary>
        /// Gets the current domain without the Protocol
        /// </summary>
        /// <returns></returns>
        protected string CurrentDomainWithoutProtocol()
        {
            return CurrentDomain(false);
        }

        /// <summary>
        /// Gets the current domain with or without the Protocol
        /// </summary>
        /// <returns></returns>
        private string CurrentDomain(bool includeProtocol)
        {
            var builder = new UriBuilder(Request.Url.Scheme, Request.Url.Host, Request.Url.Port);
            var domain = builder.Uri.ToString().TrimEnd('/');

            if (includeProtocol == false)
            {
                return StripHttpProtocol(domain);
            }
            return domain;
        }

        /// <summary>
        /// Removes Http or Https from url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string StripHttpProtocol(string url)
        {
            return url.Replace("http://", "").Replace("https://", "");
        }

        public void Dispose() { }
    }
}
