using JNCC.PublicWebsite.Core.Attributes.WebApi;
using JNCC.PublicWebsite.Core.Services;
using System;
using System.Net;
using System.Web.Http;
using Umbraco.Core.Models;
using Umbraco.Web.WebApi;

namespace JNCC.PublicWebsite.Core.Controllers.ApiControllers
{
    public sealed class ImagePickerApiController : UmbracoApiController
    {
        [HttpGet]
        [SimpleEnableCors]
        public IHttpActionResult Get(int id)
        {
            var leftPart = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            var mediaUrlResolverService = new FullUrlResolverService(UmbracoContext);
            var service = new ImagePickerApiService(Umbraco, mediaUrlResolverService);
            IPublishedContent root;

            if (service.TryFindRoot(id, out root) == false)
            {
                if (root == null)
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(HttpStatusCode.UnsupportedMediaType);
                }
            }

            var images = service.GetImages(root);

            return Ok(images);
        }
    }
}
