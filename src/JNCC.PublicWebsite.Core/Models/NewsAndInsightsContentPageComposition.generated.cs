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
	// Mixin content Type 1098 with alias "newsAndInsightsContentPageComposition"
	/// <summary>News and Insights Content Page Composition</summary>
	public partial interface INewsAndInsightsContentPageComposition : IPublishedContent
	{
		/// <summary>Main Content</summary>
		IHtmlString MainContent { get; }

		/// <summary>Publish Date</summary>
		DateTime PublishDate { get; }
	}

	/// <summary>News and Insights Content Page Composition</summary>
	[PublishedContentModel("newsAndInsightsContentPageComposition")]
	public partial class NewsAndInsightsContentPageComposition : PublishedContentModel, INewsAndInsightsContentPageComposition
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "newsAndInsightsContentPageComposition";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public NewsAndInsightsContentPageComposition(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<NewsAndInsightsContentPageComposition, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Main Content: The main content for the page.
		///</summary>
		[ImplementPropertyType("mainContent")]
		public IHtmlString MainContent
		{
			get { return GetMainContent(this); }
		}

		/// <summary>Static getter for Main Content</summary>
		public static IHtmlString GetMainContent(INewsAndInsightsContentPageComposition that) { return that.GetPropertyValue<IHtmlString>("mainContent"); }

		///<summary>
		/// Publish Date: This is the displayed publish date of the page. It may also also be used for sorting the article chronologically.
		///</summary>
		[ImplementPropertyType("publishDate")]
		public DateTime PublishDate
		{
			get { return GetPublishDate(this); }
		}

		/// <summary>Static getter for Publish Date</summary>
		public static DateTime GetPublishDate(INewsAndInsightsContentPageComposition that) { return that.GetPropertyValue<DateTime>("publishDate"); }
	}
}