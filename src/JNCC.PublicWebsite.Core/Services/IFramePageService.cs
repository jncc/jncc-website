using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Providers;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class IFramePageService
    {
        private readonly NavigationItemService _navigationItemService;

        public IFramePageService(NavigationItemService navigationItemService)
        {
            _navigationItemService = navigationItemService;
        }

        public IFramePageViewModel GetViewModel(IFramePage model, Uri currentUrl)
        {
            return new IFramePageViewModel()
            {
                Navigation = GetNavigation(model),
                SourceUrl = GetSourceUrl(model, currentUrl),
                CookieError = model.CookieDisabledError,
                Headline = model.GetHeadline(),
                Sections = GetSectionViewModels(model.FallbackContent),
                showOnMedium = model.DisplayOnMediumDevices,
                showOnSmall = model.DisplayOnSmallDevices,
                Content = model,
            };
        }

        private MainNavigationViewModel GetNavigation(IFramePage model)
        {
            return new MainNavigationViewModel()
            {
                Items = _navigationItemService.GetViewModels<MainNavigationItemViewModel>(model.Navigation),
                HasPageHero = false
            };
        }

        private string GetSourceUrl(IFramePage model, Uri currentUrl)
        {
            if (model.PassthroughQuerystringParameters == false)
            {
                return model.SourceUrl;
            }

            var sourceUrlBuilder = new UriBuilder(model.SourceUrl);
            var currentUrlBuilder = new UriBuilder(currentUrl);

            var sourceUrlQuery = HttpUtility.ParseQueryString(sourceUrlBuilder.Query);
            var currentUrlQuery = HttpUtility.ParseQueryString(currentUrlBuilder.Query);

            sourceUrlBuilder.Query = BuildNewQueryStringFavouringCurrentUrlQueryValues(sourceUrlQuery, currentUrlQuery);
            return sourceUrlBuilder.ToString();
        }

        private string BuildNewQueryStringFavouringCurrentUrlQueryValues(NameValueCollection sourceUrlQuery, NameValueCollection currentUrlQuery)
        {
            var allKeys = new List<string>();
            allKeys.AddRange(currentUrlQuery.AllKeys);
            allKeys.AddRange(sourceUrlQuery.AllKeys);

            var newQuery = new StringBuilder();

            foreach (var key in allKeys.Distinct())
            {
                var newValue = currentUrlQuery.Get(key);
                var existingValue = sourceUrlQuery.Get(key);
                var actualValue = string.Empty;

                if (string.IsNullOrWhiteSpace(newValue) == false)
                {
                    actualValue = newValue;
                }
                else if (string.IsNullOrWhiteSpace(existingValue) == false)
                {
                    actualValue = existingValue;
                }

                newQuery.AppendFormat("{0}={1}&", key, actualValue);
            }

            return newQuery.ToString().TrimEnd('&');
        }

        private IEnumerable<ScienceIFrameSectionViewModel> GetSectionViewModels(IEnumerable<ScienceIframeSectionBaseSchema> mainContent)
        {
            var viewModels = new List<ScienceIFrameSectionViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(mainContent))
            {
                return viewModels;
            }

            foreach (var section in mainContent)
            {
                ScienceIFrameSectionViewModel viewModel = null;

                switch (section)
                {
                    case ScienceIframeSectionRichTextSchema richText:
                        viewModel = CreateRichTextSection(richText);
                        break;
                    case ScienceIframeSectionImageGallerySchema imageGallery:
                        viewModel = CreateImageGallerySection(imageGallery);
                        break;
                    case ScienceIframeSectionImageTextSchema imageRichText:
                        viewModel = CreateImageRichTextSection(imageRichText);
                        break;

                }

                if (viewModel != null)
                {
                    viewModels.Add(viewModel);
                }
            }

            return viewModels;
        }

        private IEnumerable<ScienceIFrameSubSectionViewModel> GetSubSectionViewModels(IEnumerable<IPublishedContent> subSections, string parentHtmlId)
        {
            var viewModels = new List<ScienceIFrameSubSectionViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(subSections))
            {
                return viewModels;
            }

            foreach (var section in subSections)
            {
                ScienceIFrameSubSectionViewModel viewModel = null;

                switch (section)
                {
                    case ScienceIframeSubSectionRichTextSchema richText:
                        viewModel = CreateRichTextSubSection(richText, parentHtmlId);
                        break;
                    case ScienceIframeSubSectionImageGallerySchema imageGallery:
                        viewModel = CreateImageGallerySubSection(imageGallery, parentHtmlId);
                        break;
                    case ScienceIframeSubSectionImageRichTextSchema imageRichText:
                        viewModel = CreateImageRichTextSubSection(imageRichText, parentHtmlId);
                        break;
                }

                if (viewModel != null)
                {
                    viewModels.Add(viewModel);
                }
            }

            return viewModels;
        }

        private TViewModel CreateSection<TViewModel>(ScienceIframeSectionBaseSchema schema, string parentSectionHtmlId = null) where TViewModel : ScienceIFrameSectionViewModelBase, new()
        {
            var section = new TViewModel()
            {
                Headline = schema.Headline,
                PartialViewName = GetPartialViewName(schema),
                HideHeadline = schema.HideHeadline
            };

            var sectionHtmlId = schema.Headline.ToUrlSegment();

            if (string.IsNullOrWhiteSpace(parentSectionHtmlId))
            {
                section.HtmlId = sectionHtmlId;
            }
            else
            {
                section.HtmlId = string.Join("-", parentSectionHtmlId, sectionHtmlId);
            }


            return section;
        }

        private string GetPartialViewName(ScienceIframeSectionBaseSchema schema)
        {
            switch (schema.DocumentTypeAlias)
            {

                case ScienceIframeSectionRichTextSchema.ModelTypeAlias:
                case ScienceIframeSubSectionRichTextSchema.ModelTypeAlias:
                    return ScienceIFramePartialViewNames.RichText;
                case ScienceIframeSubSectionImageGallerySchema.ModelTypeAlias:
                case ScienceIframeSectionImageGallerySchema.ModelTypeAlias:
                    return ScienceIFramePartialViewNames.ImageGallery;
                case ScienceIframeSectionImageTextSchema.ModelTypeAlias:
                case ScienceIframeSubSectionImageRichTextSchema.ModelTypeAlias:
                    return ScienceIFramePartialViewNames.ImageRichText;
                default:
                    throw new NotSupportedException($"Document Type, {schema.DocumentTypeAlias}, is not currently supported.");
            }
        }

        private ScienceIFrameRichTextSectionViewModel CreateRichTextSection(ScienceIframeSectionRichTextSchema schema)
        {
            var model = CreateSection<ScienceIFrameRichTextSectionViewModel>(schema);

            model.Content = schema.Content;

            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        private ScienceIFrameImageGallerySectionViewModel CreateImageGallerySection(ScienceIframeSectionImageGallerySchema schema)
        {
            var model = CreateSection<ScienceIFrameImageGallerySectionViewModel>(schema);

            model.Images = CreateSectionImageGallery(schema.Images);

            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        private ScienceIFrameRichTextSubSectionViewModel CreateRichTextSubSection(ScienceIframeSubSectionRichTextSchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ScienceIFrameRichTextSubSectionViewModel>(schema, parentSectionHtmlId);

            model.Content = schema.Content;

            return model;
        }

        private ScienceIFrameImageGallerySubSectionViewModel CreateImageGallerySubSection(ScienceIframeSubSectionImageGallerySchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ScienceIFrameImageGallerySubSectionViewModel>(schema, parentSectionHtmlId);

            model.Images = CreateSectionImageGallery(schema.Images);

            return model;
        }

        private IEnumerable<ImageGalleryItemViewModel> CreateSectionImageGallery(IEnumerable<IPublishedContent> images)
        {
            var viewModels = new List<ImageGalleryItemViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(images))
            {
                return viewModels;
            }

            foreach (var image in images)
            {
                var viewModel = new ImageGalleryItemViewModel()
                {
                    Url = image.Url,
                    ThumbnailImageUrl = image.GetCropUrl(cropAlias: ImageCropAliases.Square),
                    AlternativeText = image.Name
                };

                if (string.IsNullOrEmpty(image.Url) == false)
                {
                    viewModels.Add(viewModel);
                }
            }

            return viewModels;
        }

        private ScienceIFrameImageRichTextSectionViewModel CreateImageRichTextSection(ScienceIframeSectionImageTextSchema schema)
        {
            var model = CreateSection<ScienceIFrameImageRichTextSectionViewModel>(schema);

            model.Content = schema.Content;
            if (schema.Image != null)
            {
                model.Image = new ImageViewModel()
                {
                    Url = schema.Image.Url,
                    AlternativeText = schema.Image.Name
                };
            }
            else
            {
                model.Image = new ImageViewModel()
                {
                    Url = null,
                    AlternativeText = null
                };
            }
            model.ImagePosition = schema.GetPropertyValue<string>("imagePosition");

            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        private ScienceIFrameImageRichTextSubSectionViewModel CreateImageRichTextSubSection(ScienceIframeSubSectionImageRichTextSchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ScienceIFrameImageRichTextSubSectionViewModel>(schema, parentSectionHtmlId);

            model.Content = schema.Content;
            if (schema.Image != null)
            {
                model.Image = new ImageViewModel()
                {
                    Url = schema.Image.Url,
                    AlternativeText = schema.Image.Name
                };
            }
            else
            {
                model.Image = new ImageViewModel()
                {
                    Url = null,
                    AlternativeText = null
                };
            }
            model.ImagePosition = schema.GetPropertyValue<string>("imagePosition");

            return model;
        }

    }
}
