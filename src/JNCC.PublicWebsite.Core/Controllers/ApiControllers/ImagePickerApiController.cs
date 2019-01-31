﻿using JNCC.PublicWebsite.Core.Services;
using System.Net;
using System.Web.Http;
using Umbraco.Core.Models;
using Umbraco.Web.WebApi;

namespace JNCC.PublicWebsite.Core.Controllers.ApiControllers
{
    public sealed class ImagePickerApiController : UmbracoApiController
    {
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var service = new ImagePickerApiService(Umbraco);
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

            return Ok();
        }
    }
}
