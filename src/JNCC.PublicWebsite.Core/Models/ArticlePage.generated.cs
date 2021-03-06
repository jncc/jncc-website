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
	/// <summary>Article Page</summary>
	[PublishedContentModel("ArticlePage")]
	public partial class ArticlePage : PublishedContentModel, IPageHeroComposition, IPageSpecificIncludesComposition, IRelatedItemsComposition, ISeoComposition, ISidebarComposition
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "ArticlePage";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public ArticlePage(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<ArticlePage, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Team(s): The team(s) related or involved in this article page.
		///</summary>
		[ImplementPropertyType("articleTeams")]
		public IEnumerable<string> ArticleTeams
		{
			get { return this.GetPropertyValue<IEnumerable<string>>("articleTeams"); }
		}

		///<summary>
		/// Article Type: Classifies the article so it can be filtered by users.
		///</summary>
		[ImplementPropertyType("articleType")]
		public string ArticleType
		{
			get { return this.GetPropertyValue<string>("articleType"); }
		}

		///<summary>
		/// Content: A brief paragraph describing this article when listed on the News and Insights Landing Page.
		///</summary>
		[ImplementPropertyType("landingPageContent")]
		public IHtmlString LandingPageContent
		{
			get { return this.GetPropertyValue<IHtmlString>("landingPageContent"); }
		}

		///<summary>
		/// Main Content: The main content for the page.
		///</summary>
		[ImplementPropertyType("mainContent")]
		public IHtmlString MainContent
		{
			get { return this.GetPropertyValue<IHtmlString>("mainContent"); }
		}

		///<summary>
		/// Publish Date: This is the displayed publish date of the page. It may also also be used for sorting the article chronologically.
		///</summary>
		[ImplementPropertyType("publishDate")]
		public DateTime PublishDate
		{
			get { return this.GetPropertyValue<DateTime>("publishDate"); }
		}

		///<summary>
		/// Headline: A headline that appears above the main content of the page.  If no value is provided the page name will be used instead.  If a hero image is also provided then this headline appears over the hero image. Otherwise it appears just above the main content.
		///</summary>
		[ImplementPropertyType("headline")]
		public string Headline
		{
			get { return JNCC.PublicWebsite.Core.Models.PageHeroComposition.GetHeadline(this); }
		}

		///<summary>
		/// Hero Image: The hero image which is displayed above the main content of the page.
		///</summary>
		[ImplementPropertyType("heroImage")]
		public IPublishedContent HeroImage
		{
			get { return JNCC.PublicWebsite.Core.Models.PageHeroComposition.GetHeroImage(this); }
		}

		///<summary>
		/// HTML Lang Ref: This field should be used when the page is of another language.  When a value is not present in this field, the default value will be "en-GB".
		///</summary>
		[ImplementPropertyType("hTMLLangRef")]
		public string HTmllangRef
		{
			get { return JNCC.PublicWebsite.Core.Models.PageSpecificIncludesComposition.GetHTmllangRef(this); }
		}

		///<summary>
		/// Page-specific BODY Includes: Authored code includes which will only appear on this page and will be rendered at the end of the BODY tag in the HTML.  This is useful for adding tracking code. Styling should not be authored here and should instead be authored in the head.  This should be edited by administrators only.
		///</summary>
		[ImplementPropertyType("pageSpecificBodyIncludes")]
		public string PageSpecificBodyIncludes
		{
			get { return JNCC.PublicWebsite.Core.Models.PageSpecificIncludesComposition.GetPageSpecificBodyIncludes(this); }
		}

		///<summary>
		/// Page-specific HEAD Includes: Authored code includes which will only appear on this page and will be rendered at the end of the HEAD tag in the HTML.  This is useful for adding tracking code and style elements.  This should be edited by administrators only.
		///</summary>
		[ImplementPropertyType("pageSpecificHeadIncludes")]
		public string PageSpecificHeadIncludes
		{
			get { return JNCC.PublicWebsite.Core.Models.PageSpecificIncludesComposition.GetPageSpecificHeadIncludes(this); }
		}

		///<summary>
		/// Items: Provides related items for the current page. These items are manually authored. A maximum of 3 items can be authored.  If less than 3 items are provided then a query will be made to find the remaining items needed.
		///</summary>
		[ImplementPropertyType("relatedItems")]
		public IEnumerable<IPublishedContent> RelatedItems
		{
			get { return JNCC.PublicWebsite.Core.Models.RelatedItemsComposition.GetRelatedItems(this); }
		}

		///<summary>
		/// Search Query: A search term used to find related items. If no term is used the page headline or name will be used instead.
		///</summary>
		[ImplementPropertyType("relatedItemsSearchQuery")]
		public string RelatedItemsSearchQuery
		{
			get { return JNCC.PublicWebsite.Core.Models.RelatedItemsComposition.GetRelatedItemsSearchQuery(this); }
		}

		///<summary>
		/// Show Related Items
		///</summary>
		[ImplementPropertyType("showRelatedItems")]
		public bool ShowRelatedItems
		{
			get { return JNCC.PublicWebsite.Core.Models.RelatedItemsComposition.GetShowRelatedItems(this); }
		}

		///<summary>
		/// NoIndex: The default value for this is False, if the checkbox is set to true the NoIndex property will be added to this page
		///</summary>
		[ImplementPropertyType("noIndex")]
		public bool NoIndex
		{
			get { return JNCC.PublicWebsite.Core.Models.SeoComposition.GetNoIndex(this); }
		}

		///<summary>
		/// SEO Settings
		///</summary>
		[ImplementPropertyType("seoSettings")]
		public SEOChecker.MVC.MetaData SeoSettings
		{
			get { return JNCC.PublicWebsite.Core.Models.SeoComposition.GetSeoSettings(this); }
		}

		///<summary>
		/// Data Hub Query: An optional query which pulls links from the data hub.  If no data hub query is authored or no items are found from the data hub query then no links will be displayed.
		///</summary>
		[ImplementPropertyType("sidebarDataHubQuery")]
		public string SidebarDataHubQuery
		{
			get { return JNCC.PublicWebsite.Core.Models.SidebarComposition.GetSidebarDataHubQuery(this); }
		}

		///<summary>
		/// Primary Call To Action Button: Link & Text for an optional Call to Action button.  This could be various purposes, for example "Get in Touch" or "Download Data".
		///</summary>
		[ImplementPropertyType("sidebarPrimaryCallToActionButton")]
		public RJP.MultiUrlPicker.Models.Link SidebarPrimaryCallToActionButton
		{
			get { return JNCC.PublicWebsite.Core.Models.SidebarComposition.GetSidebarPrimaryCallToActionButton(this); }
		}

		///<summary>
		/// See Also Links: Useful links to other internal & external web pages.
		///</summary>
		[ImplementPropertyType("sidebarSeeAlsoLinks")]
		public IEnumerable<RJP.MultiUrlPicker.Models.Link> SidebarSeeAlsoLinks
		{
			get { return JNCC.PublicWebsite.Core.Models.SidebarComposition.GetSidebarSeeAlsoLinks(this); }
		}
	}
}
