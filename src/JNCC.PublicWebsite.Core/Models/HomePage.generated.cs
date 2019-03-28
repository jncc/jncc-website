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
	/// <summary>Home Page</summary>
	[PublishedContentModel("homePage")]
	public partial class HomePage : PublishedContentModel, IGlobalIncludesComposition, INavigationSettingsComposition, IPageHeroCarouselComposition, IPageSpecificIncludesComposition, ISeoComposition
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "homePage";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public HomePage(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<HomePage, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Content: Content to be displayed informing users of the website cookie policy
		///</summary>
		[ImplementPropertyType("cookieBannerContent")]
		public IHtmlString CookieBannerContent
		{
			get { return this.GetPropertyValue<IHtmlString>("cookieBannerContent"); }
		}

		///<summary>
		/// Content: A content area useful display for displaying legal & copyright information within the footer of the page.
		///</summary>
		[ImplementPropertyType("footerContent")]
		public IHtmlString FooterContent
		{
			get { return this.GetPropertyValue<IHtmlString>("footerContent"); }
		}

		///<summary>
		/// Social Media Links: Links to various social media channels.
		///</summary>
		[ImplementPropertyType("footerSocialMediaLinks")]
		public Umbraco.Web.Models.RelatedLinks FooterSocialMediaLinks
		{
			get { return this.GetPropertyValue<Umbraco.Web.Models.RelatedLinks>("footerSocialMediaLinks"); }
		}

		///<summary>
		/// Uncategorised Links: Uncategorised links that appear at the very bottom of the footer.
		///</summary>
		[ImplementPropertyType("footerUncategorisedLinks")]
		public IEnumerable<RJP.MultiUrlPicker.Models.Link> FooterUncategorisedLinks
		{
			get { return this.GetPropertyValue<IEnumerable<RJP.MultiUrlPicker.Models.Link>>("footerUncategorisedLinks"); }
		}

		///<summary>
		/// Member Account Page: The page logged in members will be able to manage their account.
		///</summary>
		[ImplementPropertyType("memberAccountPage")]
		public IPublishedContent MemberAccountPage
		{
			get { return this.GetPropertyValue<IPublishedContent>("memberAccountPage"); }
		}

		///<summary>
		/// Fallback Image: This is used by related items that do not have an image associated with them .
		///</summary>
		[ImplementPropertyType("relatedItemsFallbackImage")]
		public IPublishedContent RelatedItemsFallbackImage
		{
			get { return this.GetPropertyValue<IPublishedContent>("relatedItemsFallbackImage"); }
		}

		///<summary>
		/// Title: A title to be given to the resources section.
		///</summary>
		[ImplementPropertyType("resourcesTitle")]
		public string ResourcesTitle
		{
			get { return this.GetPropertyValue<string>("resourcesTitle"); }
		}

		///<summary>
		/// Show Latest News
		///</summary>
		[ImplementPropertyType("showLatestNews")]
		public bool ShowLatestNews
		{
			get { return this.GetPropertyValue<bool>("showLatestNews"); }
		}

		///<summary>
		/// Show Social Feed
		///</summary>
		[ImplementPropertyType("showSocialFeed")]
		public bool ShowSocialFeed
		{
			get { return this.GetPropertyValue<bool>("showSocialFeed"); }
		}

		///<summary>
		/// Twitter Feed URL: If no URL is authored, the Social Feed will not be displayed even if the "Show Social Feed" is enabled.  URL format must begin with https://twitter.com/ followed by the Twitter account handle.  Example: https://twitter.com/jncc_uk
		///</summary>
		[ImplementPropertyType("twitterFeedURL")]
		public string TwitterFeedUrl
		{
			get { return this.GetPropertyValue<string>("twitterFeedURL"); }
		}

		///<summary>
		/// Global BODY Includes: Authored code includes which will appear on each page and will be rendered at the end of the BODY tag in the HTML.  This is useful for adding tracking code. Styling should not be authored here and should instead be authored in the head.  This should be edited by administrators only.
		///</summary>
		[ImplementPropertyType("globalBodyIncludes")]
		public string GlobalBodyIncludes
		{
			get { return JNCC.PublicWebsite.Core.Models.GlobalIncludesComposition.GetGlobalBodyIncludes(this); }
		}

		///<summary>
		/// Global HEAD Includes: Authored code includes which will appear on each page and will be rendered at the end of the HEAD tag in the HTML.  This is useful for adding tracking code and style elements.  This should be edited by administrators only.
		///</summary>
		[ImplementPropertyType("globalHeadIncludes")]
		public string GlobalHeadIncludes
		{
			get { return JNCC.PublicWebsite.Core.Models.GlobalIncludesComposition.GetGlobalHeadIncludes(this); }
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
		/// Hide Children from Navigation: Hides any child pages from the main navigation.
		///</summary>
		[ImplementPropertyType("umbracoNaviHideChildren")]
		public bool UmbracoNaviHideChildren
		{
			get { return JNCC.PublicWebsite.Core.Models.NavigationSettingsComposition.GetUmbracoNaviHideChildren(this); }
		}

		///<summary>
		/// Headline: A headline which appears within the Hero Carousel.
		///</summary>
		[ImplementPropertyType("headline")]
		public string Headline
		{
			get { return JNCC.PublicWebsite.Core.Models.PageHeroCarouselComposition.GetHeadline(this); }
		}

		///<summary>
		/// Hero Content: A brief paragraph to be displayed within the hero carousel.
		///</summary>
		[ImplementPropertyType("heroContent")]
		public IHtmlString HeroContent
		{
			get { return JNCC.PublicWebsite.Core.Models.PageHeroCarouselComposition.GetHeroContent(this); }
		}

		///<summary>
		/// Hero Images: Images to be displayed in the hero image carousel
		///</summary>
		[ImplementPropertyType("heroImages")]
		public IEnumerable<IPublishedContent> HeroImages
		{
			get { return JNCC.PublicWebsite.Core.Models.PageHeroCarouselComposition.GetHeroImages(this); }
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
		/// SEO Settings
		///</summary>
		[ImplementPropertyType("seoSettings")]
		public SEOChecker.MVC.MetaData SeoSettings
		{
			get { return JNCC.PublicWebsite.Core.Models.SeoComposition.GetSeoSettings(this); }
		}
	}
}
