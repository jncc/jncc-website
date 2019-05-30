using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Providers;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System;
using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class ScienceCategoryPageService
    {
        private IScienceDetailsPageProvider _pagesProvider;

        public ScienceCategoryPageService(IScienceDetailsPageProvider pagesProvider)
        {
            _pagesProvider = pagesProvider;
        }

        public ScienceCategoryPageViewModel GetViewModel(ScienceCategoryPage scienceCategoryPage)
        {
            return new ScienceCategoryPageViewModel()
            {
                Headline = scienceCategoryPage.GetHeadline(),
                Preamble = scienceCategoryPage.Preamble,
                Sections = GetSectionViewModels(scienceCategoryPage.MainContent),
                CategorisedPages = GetCategorisedPages(scienceCategoryPage),
                RelatedCategories = GetRelatedCategories(scienceCategoryPage)
            };
        }

        private IReadOnlyDictionary<char, IEnumerable<NavigationItemViewModel>> GetCategorisedPages(ScienceCategoryPage scienceCategoryPage)
        {
            var pages = _pagesProvider.GetByCategory(scienceCategoryPage);

            if (ExistenceUtility.IsNullOrEmpty(pages))
            {
                return null;
            }

            return pages.CategorisePages();
        }

        private IReadOnlyDictionary<char, IEnumerable<NavigationItemViewModel>> GetRelatedCategories(ScienceCategoryPage scienceCategoryPage)
        {
            if (ExistenceUtility.IsNullOrEmpty(scienceCategoryPage.RelatedCategories))
            {
                return null;
            }

            return scienceCategoryPage.RelatedCategories.CategorisePages();
        }

        private IEnumerable<ScienceCategorySectionViewModel> GetSectionViewModels(IEnumerable<ScienceCategorySectionBaseSchema> mainContent)
        {
            var viewModels = new List<ScienceCategorySectionViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(mainContent))
            {
                return viewModels;
            }

            foreach (var section in mainContent)
            {
                ScienceCategorySectionViewModel viewModel = null;

                switch (section)
                {
                    case ScienceCategorySectionRichTextSchema richText:
                        viewModel = CreateRichTextSection(richText);
                        break;
                    case ScienceCategorySectionImageGallerySchema imageGallery:
                        viewModel = CreateImageGallerySection(imageGallery);
                        break;
                }

                if (viewModel != null)
                {
                    viewModels.Add(viewModel);
                }
            }

            return viewModels;
        }

        private IEnumerable<ScienceCategorySubSectionViewModel> GetSubSectionViewModels(IEnumerable<IPublishedContent> subSections, string parentHtmlId)
        {
            var viewModels = new List<ScienceCategorySubSectionViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(subSections))
            {
                return viewModels;
            }

            foreach (var section in subSections)
            {
                ScienceCategorySubSectionViewModel viewModel = null;

                switch (section)
                {
                    case ScienceCategorySubSectionRichTextSchema richText:
                        viewModel = CreateRichTextSubSection(richText, parentHtmlId);
                        break;
                    case ScienceCategorySubSectionImageGallerySchema imageGallery:
                        viewModel = CreateImageGallerySubSection(imageGallery, parentHtmlId);
                        break;
                }

                if (viewModel != null)
                {
                    viewModels.Add(viewModel);
                }
            }

            return viewModels;
        }

        private TViewModel CreateSection<TViewModel>(ScienceCategorySectionBaseSchema schema, string parentSectionHtmlId = null) where TViewModel : ScienceCategorySectionViewModelBase, new()
        {
            var section = new TViewModel()
            {
                Headline = schema.Headline,
                PartialViewName = GetPartialViewName(schema)
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

        private string GetPartialViewName(ScienceCategorySectionBaseSchema schema)
        {
            switch (schema.DocumentTypeAlias)
            {
                case ScienceCategorySectionRichTextSchema.ModelTypeAlias:
                case ScienceCategorySubSectionRichTextSchema.ModelTypeAlias:
                    return ScienceCategoryPartialViewNames.RichText;
                case ScienceCategorySubSectionImageGallerySchema.ModelTypeAlias:
                case ScienceCategorySectionImageGallerySchema.ModelTypeAlias:
                    return ScienceCategoryPartialViewNames.ImageGallery;
                default:
                    throw new NotSupportedException($"Document Type, {schema.DocumentTypeAlias}, is not currently supported.");
            }
        }

        private ScienceCategoryRichTextSectionViewModel CreateRichTextSection(ScienceCategorySectionRichTextSchema schema)
        {
            var model = CreateSection<ScienceCategoryRichTextSectionViewModel>(schema);

            model.Content = schema.Content;

            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        private ScienceCategoryImageGallerySectionViewModel CreateImageGallerySection(ScienceCategorySectionImageGallerySchema schema)
        {
            var model = CreateSection<ScienceCategoryImageGallerySectionViewModel>(schema);

            model.Images = CreateSectionImageGallery(schema.Images);

            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        private ScienceCategoryRichTextSubSectionViewModel CreateRichTextSubSection(ScienceCategorySubSectionRichTextSchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ScienceCategoryRichTextSubSectionViewModel>(schema, parentSectionHtmlId);

            model.Content = schema.Content;

            return model;
        }

        private ScienceCategoryImageGallerySubSectionViewModel CreateImageGallerySubSection(ScienceCategorySubSectionImageGallerySchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ScienceCategoryImageGallerySubSectionViewModel>(schema, parentSectionHtmlId);

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
    }
}
