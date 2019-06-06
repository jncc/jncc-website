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
	// Mixin content Type 1128 with alias "seoComposition"
	/// <summary>SEO Composition</summary>
	public partial interface ISeoComposition : IPublishedContent
	{
		/// <summary>NoIndex</summary>
		bool NoIndex { get; }

		/// <summary>SEO Settings</summary>
		SEOChecker.MVC.MetaData SeoSettings { get; }
	}

	/// <summary>SEO Composition</summary>
	[PublishedContentModel("seoComposition")]
	public partial class SeoComposition : PublishedContentModel, ISeoComposition
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "seoComposition";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public SeoComposition(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<SeoComposition, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// NoIndex: The default value for this is False, if the checkbox is set to true the NoIndex property will be added to this page
		///</summary>
		[ImplementPropertyType("noIndex")]
		public bool NoIndex
		{
			get { return GetNoIndex(this); }
		}

		/// <summary>Static getter for NoIndex</summary>
		public static bool GetNoIndex(ISeoComposition that) { return that.GetPropertyValue<bool>("noIndex"); }

		///<summary>
		/// SEO Settings
		///</summary>
		[ImplementPropertyType("seoSettings")]
		public SEOChecker.MVC.MetaData SeoSettings
		{
			get { return GetSeoSettings(this); }
		}

		/// <summary>Static getter for SEO Settings</summary>
		public static SEOChecker.MVC.MetaData GetSeoSettings(ISeoComposition that) { return that.GetPropertyValue<SEOChecker.MVC.MetaData>("seoSettings"); }
	}
}
