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
	// Mixin content Type 1160 with alias "pageMetaInformationComposition"
	/// <summary>Page Meta Information Composition</summary>
	public partial interface IPageMetaInformationComposition : IPublishedContent
	{
		/// <summary>Published Date</summary>
		DateTime PublishedDate { get; }

		/// <summary>Reviewed Date</summary>
		DateTime ReviewedDate { get; }
	}

	/// <summary>Page Meta Information Composition</summary>
	[PublishedContentModel("pageMetaInformationComposition")]
	public partial class PageMetaInformationComposition : PublishedContentModel, IPageMetaInformationComposition
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "pageMetaInformationComposition";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public PageMetaInformationComposition(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<PageMetaInformationComposition, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Published Date: The date is when the page was first published.   This is a required property as a page with a Meta Information must have a published date.
		///</summary>
		[ImplementPropertyType("publishedDate")]
		public DateTime PublishedDate
		{
			get { return GetPublishedDate(this); }
		}

		/// <summary>Static getter for Published Date</summary>
		public static DateTime GetPublishedDate(IPageMetaInformationComposition that) { return that.GetPropertyValue<DateTime>("publishedDate"); }

		///<summary>
		/// Reviewed Date: The date the page last had a meaningful editorial review.
		///</summary>
		[ImplementPropertyType("reviewedDate")]
		public DateTime ReviewedDate
		{
			get { return GetReviewedDate(this); }
		}

		/// <summary>Static getter for Reviewed Date</summary>
		public static DateTime GetReviewedDate(IPageMetaInformationComposition that) { return that.GetPropertyValue<DateTime>("reviewedDate"); }
	}
}