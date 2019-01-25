//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder v3.0.10.102
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.ModelsBuilder;
using Umbraco.ModelsBuilder.Umbraco;

namespace JNCC.PublicWebsite.Core.Models
{
	/// <summary>Articulate</summary>
	[PublishedContentModel("Articulate")]
	public partial class Articulate : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "Articulate";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public Articulate(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<Articulate, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Blog Banner: Optional blog banner
		///</summary>
		[ImplementPropertyType("blogBanner")]
		public Umbraco.Web.Models.ImageCropDataSet BlogBanner
		{
			get { return this.GetPropertyValue<Umbraco.Web.Models.ImageCropDataSet>("blogBanner"); }
		}

		///<summary>
		/// Blog Description
		///</summary>
		[ImplementPropertyType("blogDescription")]
		public string BlogDescription
		{
			get { return this.GetPropertyValue<string>("blogDescription"); }
		}

		///<summary>
		/// Blog Logo: Optional logo
		///</summary>
		[ImplementPropertyType("blogLogo")]
		public Umbraco.Web.Models.ImageCropDataSet BlogLogo
		{
			get { return this.GetPropertyValue<Umbraco.Web.Models.ImageCropDataSet>("blogLogo"); }
		}

		///<summary>
		/// Blog Title
		///</summary>
		[ImplementPropertyType("blogTitle")]
		public string BlogTitle
		{
			get { return this.GetPropertyValue<string>("blogTitle"); }
		}

		///<summary>
		/// Categories Page Name: The page title for the categories listing
		///</summary>
		[ImplementPropertyType("categoriesPageName")]
		public string CategoriesPageName
		{
			get { return this.GetPropertyValue<string>("categoriesPageName"); }
		}

		///<summary>
		/// Categories Url Name
		///</summary>
		[ImplementPropertyType("categoriesUrlName")]
		public string CategoriesUrlName
		{
			get { return this.GetPropertyValue<string>("categoriesUrlName"); }
		}

		///<summary>
		/// Comments Form: The Umbraco Form used by website users to submit comments to blog posts in this blog. If no form is selected users will not be able to submit comments.
		///</summary>
		[ImplementPropertyType("commentsForm")]
		public object CommentsForm
		{
			get { return this.GetPropertyValue("commentsForm"); }
		}

		///<summary>
		/// Custom RSS Feed Url: Optional custom rss feed URL (i.e. if you use feedburner, etc...)
		///</summary>
		[ImplementPropertyType("customRssFeedUrl")]
		public string CustomRssFeedUrl
		{
			get { return this.GetPropertyValue<string>("customRssFeedUrl"); }
		}

		///<summary>
		/// Disqus Shortname
		///</summary>
		[ImplementPropertyType("disqusShortname")]
		public string DisqusShortname
		{
			get { return this.GetPropertyValue<string>("disqusShortname"); }
		}

		///<summary>
		/// Enable Comments: If this is disabled then comments will not be allowed within this blog regardless of individual blog post settings.
		///</summary>
		[ImplementPropertyType("enableComments")]
		public bool EnableComments
		{
			get { return this.GetPropertyValue<bool>("enableComments"); }
		}

		///<summary>
		/// Extract First Image to Property: When Windows Live Writer (or compatible WebBlog API tool) is used to create blog posts, with this option enabled it will set the first image found in the blog post to the blog's image property if one is found.
		///</summary>
		[ImplementPropertyType("extractFirstImage")]
		public bool ExtractFirstImage
		{
			get { return this.GetPropertyValue<bool>("extractFirstImage"); }
		}

		///<summary>
		/// Google Analytics Id: Your Google Analytics Id (i.e. UA-123456789 )
		///</summary>
		[ImplementPropertyType("googleAnalyticsId")]
		public string GoogleAnalyticsId
		{
			get { return this.GetPropertyValue<string>("googleAnalyticsId"); }
		}

		///<summary>
		/// Google Analytics Name: The site name associated with your Google Analytics (i.e. mysite.com )
		///</summary>
		[ImplementPropertyType("googleAnalyticsName")]
		public string GoogleAnalyticsName
		{
			get { return this.GetPropertyValue<string>("googleAnalyticsName"); }
		}

		///<summary>
		/// PageSize
		///</summary>
		[ImplementPropertyType("pageSize")]
		public int PageSize
		{
			get { return this.GetPropertyValue<int>("pageSize"); }
		}

		///<summary>
		/// Redirect Archive: If specified this will redirect the Archive blog post container URL to this Articulate blog root
		///</summary>
		[ImplementPropertyType("redirectArchive")]
		public bool RedirectArchive
		{
			get { return this.GetPropertyValue<bool>("redirectArchive"); }
		}

		///<summary>
		/// Search Page Name: The page title for the search results
		///</summary>
		[ImplementPropertyType("searchPageName")]
		public string SearchPageName
		{
			get { return this.GetPropertyValue<string>("searchPageName"); }
		}

		///<summary>
		/// Search Url Name
		///</summary>
		[ImplementPropertyType("searchUrlName")]
		public string SearchUrlName
		{
			get { return this.GetPropertyValue<string>("searchUrlName"); }
		}

		///<summary>
		/// Tags Page Name: The page title for the tags listing
		///</summary>
		[ImplementPropertyType("tagsPageName")]
		public string TagsPageName
		{
			get { return this.GetPropertyValue<string>("tagsPageName"); }
		}

		///<summary>
		/// Tags Url Name
		///</summary>
		[ImplementPropertyType("tagsUrlName")]
		public string TagsUrlName
		{
			get { return this.GetPropertyValue<string>("tagsUrlName"); }
		}

		///<summary>
		/// Theme
		///</summary>
		[ImplementPropertyType("theme")]
		public object Theme
		{
			get { return this.GetPropertyValue("theme"); }
		}

		///<summary>
		/// Use yyyy/mm/dd format for Url: If specified, this will generate posts' urls in the /yyyy/mm/dd/slug format, ie 2017/06/09/codegarden-rocks
		///</summary>
		[ImplementPropertyType("useDateFormatForUrl")]
		public bool UseDateFormatForUrl
		{
			get { return this.GetPropertyValue<bool>("useDateFormatForUrl"); }
		}
	}
}