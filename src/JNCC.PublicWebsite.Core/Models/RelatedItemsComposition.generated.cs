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
	// Mixin content Type 1127 with alias "relatedItemsComposition"
	/// <summary>Related Items Composition</summary>
	public partial interface IRelatedItemsComposition : IPublishedContent
	{
		/// <summary>Items</summary>
		IEnumerable<IPublishedContent> RelatedItems { get; }

		/// <summary>Search Query</summary>
		string RelatedItemsSearchQuery { get; }

		/// <summary>Show Related Items</summary>
		bool ShowRelatedItems { get; }
	}

	/// <summary>Related Items Composition</summary>
	[PublishedContentModel("relatedItemsComposition")]
	public partial class RelatedItemsComposition : PublishedContentModel, IRelatedItemsComposition
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "relatedItemsComposition";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public RelatedItemsComposition(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<RelatedItemsComposition, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Items: Provides related items for the current page. These items are manually authored. A maximum of 3 items can be authored.  If less than 3 items are provided then a query will be made to find the remaining items needed.
		///</summary>
		[ImplementPropertyType("relatedItems")]
		public IEnumerable<IPublishedContent> RelatedItems
		{
			get { return GetRelatedItems(this); }
		}

		/// <summary>Static getter for Items</summary>
		public static IEnumerable<IPublishedContent> GetRelatedItems(IRelatedItemsComposition that) { return that.GetPropertyValue<IEnumerable<IPublishedContent>>("relatedItems"); }

		///<summary>
		/// Search Query: A search term used to find related items. If no term is used the page headline or name will be used instead.
		///</summary>
		[ImplementPropertyType("relatedItemsSearchQuery")]
		public string RelatedItemsSearchQuery
		{
			get { return GetRelatedItemsSearchQuery(this); }
		}

		/// <summary>Static getter for Search Query</summary>
		public static string GetRelatedItemsSearchQuery(IRelatedItemsComposition that) { return that.GetPropertyValue<string>("relatedItemsSearchQuery"); }

		///<summary>
		/// Show Related Items
		///</summary>
		[ImplementPropertyType("showRelatedItems")]
		public bool ShowRelatedItems
		{
			get { return GetShowRelatedItems(this); }
		}

		/// <summary>Static getter for Show Related Items</summary>
		public static bool GetShowRelatedItems(IRelatedItemsComposition that) { return that.GetPropertyValue<bool>("showRelatedItems"); }
	}
}
