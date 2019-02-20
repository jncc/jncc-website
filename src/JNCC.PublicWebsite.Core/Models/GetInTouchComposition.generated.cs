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
	/// <summary>Get in Touch Composition</summary>
	[PublishedContentModel("getInTouchComposition")]
	public partial class GetInTouchComposition : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "getInTouchComposition";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public GetInTouchComposition(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<GetInTouchComposition, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Get in Touch Button: The link & text for the Get in Touch button which accompanies the Get in Touch content below the main content of the page.
		///</summary>
		[ImplementPropertyType("getInTouchButton")]
		public RJP.MultiUrlPicker.Models.Link GetInTouchButton
		{
			get { return this.GetPropertyValue<RJP.MultiUrlPicker.Models.Link>("getInTouchButton"); }
		}

		///<summary>
		/// Get In Touch Content: Optional content which appears below the main content of the page. This content is specifically for encouraging website users to navigate to the contact form.
		///</summary>
		[ImplementPropertyType("getInTouchContent")]
		public IHtmlString GetInTouchContent
		{
			get { return this.GetPropertyValue<IHtmlString>("getInTouchContent"); }
		}
	}
}
