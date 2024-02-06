using JNCC.PublicWebsite.Core.Attributes.WebApi;
using JNCC.PublicWebsite.Core.Services;
using SEOChecker.Core.Extensions;
using SEOChecker.Core.Extensions.UmbracoExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using umbraco.cms.businesslogic.property;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.WebApi;

namespace JNCC.PublicWebsite.Core.Controllers.ApiControllers
{
    public sealed class PageExportApiController : UmbracoAuthorizedApiController
	{
		[HttpGet]
        [ActionName("Export")]
		public IHttpActionResult Export()
        {
			IContentService _contentService = Services.ContentService;
            IUserService _userService = Services.UserService;
            
            var pages = _contentService.GetRootContent().SelectRecursive(c => c.Descendants());
            var url = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.GetHostAndPort();

            var formatted = pages.Select(p => new PageExportModel(p, _userService, Umbraco, url));

            CSVExportService.GenericExport(formatted.ToArray(), "Pages", true);

            return InternalServerError(new Exception("There was an issue generating the report"));
        }
    }

	/*
    a csv file of all unpublished/draft page titles (or ALL pages if easier)
    published status (if extracting all pages, I don't expect there's a difference between draft and unpublished)
    page url (slug if easier)
    author
    date created
    template used (if possible)
    */
	
    public class PageExportModel
    {
        public PageExportModel(IContent model, IUserService userService, UmbracoHelper umbracoHelper, string url)
        {
            Id = model.Id;
            Name = model.Name;
            Status = model.Status.ToString();
            FrontendUrl = "";
            BackofficeUrl = url + "/umbraco#/content/content/edit/" + model.Id;
            Author = model.GetCreatorProfile(userService)?.Name;
            DateCreated = model.CreateDate.ToString("dd/MM/yyyy HH:mm");
            LastUpdated = model.UpdateDate.ToString("dd/MM/yyyy HH:mm");
            TemplateUsed = model.Template.Name;

            if (model.HasPublishedVersion)
            {
                FrontendUrl = umbracoHelper.TypedContent(model.Id)?.UrlAbsolute();
            }
		}

        public int Id { get; set; }
        public string Name { get; set; }
        public string PublishedStatus { get; set; }
        public string FrontendUrl { get; set; }
        public string BackofficeUrl { get; set; }   
        public string Author { get; set; }
        public string DateCreated { get; set; }
        public string LastUpdated { get; set; }
		public string TemplateUsed { get; set; }
	}

}
