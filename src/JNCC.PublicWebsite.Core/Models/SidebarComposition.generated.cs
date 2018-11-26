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
	// Mixin content Type 1109 with alias "sidebarComposition"
	/// <summary>Sidebar Composition</summary>
	public partial interface ISidebarComposition : IPublishedContent
	{
		/// <summary>Get in Touch Button</summary>
		RJP.MultiUrlPicker.Models.Link SidebarGetInTouchButton { get; }

		/// <summary>See Also Links</summary>
		IEnumerable<RJP.MultiUrlPicker.Models.Link> SidebarSeeAlsoLinks { get; }
	}

	/// <summary>Sidebar Composition</summary>
	[PublishedContentModel("sidebarComposition")]
	public partial class SidebarComposition : PublishedContentModel, ISidebarComposition
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "sidebarComposition";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public SidebarComposition(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<SidebarComposition, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Get in Touch Button: Link & Text for an optional Get in Touch button.
		///</summary>
		[ImplementPropertyType("sidebarGetInTouchButton")]
		public RJP.MultiUrlPicker.Models.Link SidebarGetInTouchButton
		{
			get { return GetSidebarGetInTouchButton(this); }
		}

		/// <summary>Static getter for Get in Touch Button</summary>
		public static RJP.MultiUrlPicker.Models.Link GetSidebarGetInTouchButton(ISidebarComposition that) { return that.GetPropertyValue<RJP.MultiUrlPicker.Models.Link>("sidebarGetInTouchButton"); }

		///<summary>
		/// See Also Links: Useful links to other internal & external web pages.
		///</summary>
		[ImplementPropertyType("sidebarSeeAlsoLinks")]
		public IEnumerable<RJP.MultiUrlPicker.Models.Link> SidebarSeeAlsoLinks
		{
			get { return GetSidebarSeeAlsoLinks(this); }
		}

		/// <summary>Static getter for See Also Links</summary>
		public static IEnumerable<RJP.MultiUrlPicker.Models.Link> GetSidebarSeeAlsoLinks(ISidebarComposition that) { return that.GetPropertyValue<IEnumerable<RJP.MultiUrlPicker.Models.Link>>("sidebarSeeAlsoLinks"); }
	}
}
