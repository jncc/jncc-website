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
	/// <summary>Science Category Sub Section Image Code Schema</summary>
	[PublishedContentModel("scienceCategorySubSectionImageCodeSchema")]
	public partial class ScienceCategorySubSectionImageCodeSchema : ScienceCategorySectionBaseSchema
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "scienceCategorySubSectionImageCodeSchema";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public ScienceCategorySubSectionImageCodeSchema(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<ScienceCategorySubSectionImageCodeSchema, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Content
		///</summary>
		[ImplementPropertyType("content")]
		public IHtmlString Content
		{
			get { return this.GetPropertyValue<IHtmlString>("content"); }
		}

		///<summary>
		/// Image Code
		///</summary>
		[ImplementPropertyType("imageCode")]
		public string ImageCode
		{
			get { return this.GetPropertyValue<string>("imageCode"); }
		}

		///<summary>
		/// Image Position: Accepted written positions are: 'Left', 'Right', 'Top'
		///</summary>
		[ImplementPropertyType("imagePosition")]
		public string ImagePosition
		{
			get { return this.GetPropertyValue<string>("imagePosition"); }
		}
	}
}
