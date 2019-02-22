using System.Web.Http;

namespace JNCC.PublicWebsite.Core
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();
        }
    }
}
