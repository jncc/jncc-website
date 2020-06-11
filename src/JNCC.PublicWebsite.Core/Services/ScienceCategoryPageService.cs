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
                ImageTextSection = GetImageTextSectionViewModels(scienceCategoryPage.ImageAndTextSection),
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

        private IEnumerable<ScienceCategorySectionViewModel> GetImageTextSectionViewModels(IEnumerable<ScienceCategorySectionBaseSchema> imageTextSection)
        {
            var viewModels = new List<ScienceCategorySectionViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(imageTextSection))
            {
                return viewModels;
            }

            foreach (var section in imageTextSection)
            {
                ScienceCategorySectionViewModel viewModel = null;

                switch (section)
                {
                    case ScienceCategoryIndividualSectionImageTextSchema imageRichText:
                        viewModel = CreateIndividualImageRichTextSection(imageRichText);
                        break;
                    case ScienceCategoryIndividualSectionImageCodeSchema imageCode:
                        viewModel = CreateIndividualImageCodeSection(imageCode);
                        break;
                }

                if (viewModel != null)
                {
                    viewModels.Add(viewModel);
                }
            }

            return viewModels;
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
                    case ScienceCategorySectionImageTextSchema imageRichText:
                        viewModel = CreateImageRichTextSection(imageRichText);
                        break;
                    case ScienceCategorySectionImageCodeSchema imageCode:
                        viewModel = CreateImageCodeSection(imageCode);
                        break;
                    case ScienceCategorySectionSliderSchema slider:
                        viewModel = CreateSliderSection(slider);
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
                    case ScienceCategorySubSectionImageRichTextSchema imageRichText:
                        viewModel = CreateImageRichTextSubSection(imageRichText, parentHtmlId);
                        break;
                    case ScienceCategorySubSectionImageCodeSchema imageCode:
                        viewModel = CreateImageCodeSubSection(imageCode, parentHtmlId);
                        break;
                    case ScienceCategorySubSectionSliderSchema slider:
                        viewModel = CreateSliderSubSection(slider, parentHtmlId);
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
                case ScienceCategoryIndividualSectionImageTextSchema.ModelTypeAlias:
                case ScienceCategorySectionImageTextSchema.ModelTypeAlias:
                case ScienceCategorySubSectionImageRichTextSchema.ModelTypeAlias:
                    return ScienceCategoryPartialViewNames.ImageRichText;
                case ScienceCategoryIndividualSectionImageCodeSchema.ModelTypeAlias:
                case ScienceCategorySectionImageCodeSchema.ModelTypeAlias:
                case ScienceCategorySubSectionImageCodeSchema.ModelTypeAlias:
                    return ScienceCategoryPartialViewNames.ImageCode;
                case ScienceCategorySectionSliderSchema.ModelTypeAlias:
                case ScienceCategorySubSectionSliderSchema.ModelTypeAlias:
                    return ScienceCategoryPartialViewNames.Slider;
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
                    AlternativeText = image.GetPropertyValue<string>("altText").IsNullOrWhiteSpace() ? image.Name : image.GetPropertyValue<string>("altText"),
                    TitleText = image.GetPropertyValue<string>("titleText") 
                };

                if (string.IsNullOrEmpty(image.Url) == false)
                {
                    viewModels.Add(viewModel);
                }
            }

            return viewModels;
        }

        private ScienceCategoryImageRichTextSectionViewModel CreateImageRichTextSection(ScienceCategorySectionImageTextSchema schema)
        {
            var model = CreateSection<ScienceCategoryImageRichTextSectionViewModel>(schema);

            model.Content = schema.Content;
            if (schema.Image != null)
            {
                model.Image = new ImageViewModel()
                {
                    Url = schema.Image.Url,
                    AlternativeText = schema.Image.GetPropertyValue<string>("altText").IsNullOrWhiteSpace() ? schema.Image.Name : schema.Image.GetPropertyValue<string>("altText"),
                    TitleText = schema.Image.GetPropertyValue<string>("titleText"),
                };
            }
            else
            {
                model.Image = new ImageViewModel()
                {
                    Url = null,
                    AlternativeText = null,
                    TitleText = null,
                };
            }
            model.ImagePosition = schema.GetPropertyValue<string>("imagePosition");

            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        private ScienceCategoryImageCodeSectionViewModel CreateImageCodeSection(ScienceCategorySectionImageCodeSchema schema)
        {
            var model = CreateSection<ScienceCategoryImageCodeSectionViewModel>(schema);

            model.Content = schema.Content;

            model.ImageCode = schema.ImageCode;
           
            model.ImagePosition = schema.GetPropertyValue<string>("imagePosition");

            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        private ScienceCategorySliderSectionViewModel CreateSliderSection(ScienceCategorySectionSliderSchema schema)
        {
            var model = CreateSection<ScienceCategorySliderSectionViewModel>(schema);

            model.Content = schema.Content;
            model.ShowBackground = schema.ShowGreyBackground;
            model.ShowTimelineArrows = schema.ShowTimelineArrows;
            model.SliderItems = GetSliderItemViewModels(schema.SliderItems);
            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        private IEnumerable<ScienceSliderSchemaViewModel> GetSliderItemViewModels(IEnumerable<IPublishedContent> sliderItems)
        {
            var viewModels = new List<ScienceSliderSchemaViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(sliderItems))
            {
                return viewModels;
            }

            foreach (var section in sliderItems)
            {
                ScienceSliderSchemaViewModel viewModel = null;

                switch (section)
                {
                    case ScienceSliderSchemaHeadingText headingText:
                        viewModel = CreateHeadingTextSliderItem(headingText);
                        break;
                    case ScienceSliderSchemaImageText imageText:
                        viewModel = CreateImageTextSliderItem(imageText);
                        break;
                }

                if (viewModel != null)
                {
                    viewModels.Add(viewModel);
                }
            }

            return viewModels;
        }

        private ScienceSliderSchemaViewModel CreateHeadingTextSliderItem(ScienceSliderSchemaHeadingText schema)
        {
            var model = new ScienceSliderSchemaViewModel();

            model.Heading = schema.Heading;
            model.ImageTextSection = false;
            model.Text = schema.Text;

            return model;
        }

        private ScienceSliderSchemaViewModel CreateImageTextSliderItem(ScienceSliderSchemaImageText schema)
        {
            var model = new ScienceSliderSchemaViewModel();

            model.ImageTextSection = true;
            model.Text = schema.Text;
            model.ImagePosition = schema.ImagePosition;

            if (schema.Image != null)
            {
                model.Image = new ImageViewModel()
                {
                    Url = schema.Image.Url,
                    AlternativeText = schema.Image.GetPropertyValue<string>("altText").IsNullOrWhiteSpace() ? schema.Image.Name : schema.Image.GetPropertyValue<string>("altText"),
                    TitleText = schema.Image.GetPropertyValue<string>("titleText"),
                };
            }
            else
            {
                model.Image = new ImageViewModel()
                {
                    Url = null,
                    AlternativeText = null,
                    TitleText = null
                };
            }

            if (schema.ImageLink != null)
            {
                model.ImageLink = new NavigationItemViewModel()
                {
                    Url = schema.ImageLink.Url,
                    Text = schema.ImageLink.Name,
                    Target = schema.ImageLink.Target,
                };
            }
            else
            {
                model.ImageLink = new NavigationItemViewModel(){ Url = null, Text = null, Target = null };
            }

            return model;
        }

        private ScienceCategoryImageRichTextSectionViewModel CreateIndividualImageRichTextSection(ScienceCategoryIndividualSectionImageTextSchema schema)
        {
            var model = CreateSection<ScienceCategoryImageRichTextSectionViewModel>(schema);

            model.Content = schema.Content;

            if (schema.Image != null)
            {
                model.Image = new ImageViewModel()
                {
                    Url = schema.Image.Url,
                    AlternativeText = schema.Image.GetPropertyValue<string>("altText").IsNullOrWhiteSpace() ? schema.Image.Name : schema.Image.GetPropertyValue<string>("altText"),
                    TitleText = schema.Image.GetPropertyValue<string>("titleText"),
                };
            }
            else
            {
                model.Image = new ImageViewModel()
                {
                    Url = null,
                    AlternativeText = null,
                    TitleText = null,
                };
            }


            model.ImagePosition = schema.GetPropertyValue<string>("imagePosition");

            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        private ScienceCategoryImageCodeSectionViewModel CreateIndividualImageCodeSection(ScienceCategoryIndividualSectionImageCodeSchema schema)
        {
            var model = CreateSection<ScienceCategoryImageCodeSectionViewModel>(schema);

            model.Content = schema.Content;
            
            model.ImageCode = schema.ImageCode;

            model.ImagePosition = schema.GetPropertyValue<string>("imagePosition");

            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        private ScienceCategoryImageRichTextSubSectionViewModel CreateImageRichTextSubSection(ScienceCategorySubSectionImageRichTextSchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ScienceCategoryImageRichTextSubSectionViewModel>(schema, parentSectionHtmlId);

            model.Content = schema.Content;
            if (schema.Image != null)
            {
                model.Image = new ImageViewModel()
                {
                    Url = schema.Image.Url,
                    AlternativeText = schema.Image.GetPropertyValue<string>("altText").IsNullOrWhiteSpace() ? schema.Image.Name : schema.Image.GetPropertyValue<string>("altText"),
                    TitleText = schema.Image.GetPropertyValue<string>("titleText"),
                };
            }
            else
            {
                model.Image = new ImageViewModel()
                {
                    Url = null,
                    AlternativeText = null,
                    TitleText = null,
                };
            }
            model.ImagePosition = schema.GetPropertyValue<string>("imagePosition");

            return model;
        }

        private ScienceCategoryImageCodeSubSectionViewModel CreateImageCodeSubSection(ScienceCategorySubSectionImageCodeSchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ScienceCategoryImageCodeSubSectionViewModel>(schema, parentSectionHtmlId);

            model.Content = schema.Content;

            model.ImageCode = schema.ImageCode;
           
            model.ImagePosition = schema.GetPropertyValue<string>("imagePosition");

            return model;
        }
        private ScienceCategorySliderSubSectionViewModel CreateSliderSubSection(ScienceCategorySubSectionSliderSchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ScienceCategorySliderSubSectionViewModel>(schema);

            model.Content = schema.Content;
            model.ShowBackground = schema.ShowGreyBackground;
            model.ShowTimelineArrows = schema.ShowTimelineArrows;
            model.SliderItems = GetSliderItemViewModels(schema.SliderItems);

            return model;
        }
    }
}
