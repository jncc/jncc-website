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
	/// <summary>Science Category Page</summary>
	[PublishedContentModel("scienceCategoryPage")]
	public partial class ScienceCategoryPage : PublishedContentModel, INavigationSettingsComposition, IPageHeroComposition, IPageSpecificIncludesComposition, ISciencePageCategorisationComposition, ISeoComposition
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "scienceCategoryPage";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public ScienceCategoryPage(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<ScienceCategoryPage, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Featured Pages: The main science details pages that define this section.
		///</summary>
		[ImplementPropertyType("featuredPages")]
		public IEnumerable<IPublishedContent> FeaturedPages
		{
			get { return this.GetPropertyValue<IEnumerable<IPublishedContent>>("featuredPages"); }
		}

		///<summary>
		/// Preamble: Introductory content explaining the purpose of the page.
		///</summary>
		[ImplementPropertyType("preamble")]
		public IHtmlString Preamble
		{
			get { return this.GetPropertyValue<IHtmlString>("preamble"); }
		}

		///<summary>
		/// Related Categories: Allows editors to related categories to the current category.  This is a one way process, if the linked category is also related to this one, it must be authored on the other category page too.
		///</summary>
		[ImplementPropertyType("relatedCategories")]
		public IEnumerable<IPublishedContent> RelatedCategories
		{
			get { return this.GetPropertyValue<IEnumerable<IPublishedContent>>("relatedCategories"); }
		}

		///<summary>
		/// Hide Children from Navigation: Hides any child pages from the main navigation.
		///</summary>
		[ImplementPropertyType("umbracoNavi")]
		public bool UmbracoNavi
		{
			get { return JNCC.PublicWebsite.Core.Models.NavigationSettingsComposition.GetUmbracoNavi(this); }
		}

		///<summary>
		/// Hide from Navigation: Hides the page from the main navigation.
		///</summary>
		[ImplementPropertyType("umbracoNaviHide")]
		public bool UmbracoNaviHide
		{
			get { return JNCC.PublicWebsite.Core.Models.NavigationSettingsComposition.GetUmbracoNaviHide(this); }
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
		/// Category Ordering Name: [Optional] Determines how the page is categorised on a Science Category Page.   If no value is authored then the headline is used followed by the page name.
		///</summary>
		[ImplementPropertyType("categoryOrderingName")]
		public string CategoryOrderingName
		{
			get { return JNCC.PublicWebsite.Core.Models.SciencePageCategorisationComposition.GetCategoryOrderingName(this); }
		}

		///<summary>
		/// SEO Settings
		///</summary>
		[ImplementPropertyType("seoSettings")]
		public SEOChecker.MVC.MetaData SeoSettings
		{
			get { return JNCC.PublicWebsite.Core.Models.SeoComposition.GetSeoSettings(this); }
		}
	}
}
