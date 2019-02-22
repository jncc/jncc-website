using System.Web.Http;
using System.Web.Http.Cors;

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
