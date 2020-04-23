using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Providers;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;
using JNCC.PublicWebsite.Core.PropertyValueConverters;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class ScienceDetailsPageService
    {
        private readonly NavigationItemService _navigationItemService;
        private readonly ISciencePageCategoriesProvider _sciencePageCategoriesProvider;
        public ScienceDetailsPageService(ISciencePageCategoriesProvider sciencePageCategoriesProvider, NavigationItemService navigationItemService)
        {
            _sciencePageCategoriesProvider = sciencePageCategoriesProvider;
            _navigationItemService = navigationItemService;
        }

        public ScienceDetailsPageViewModel GetViewModel(ScienceDetailsPage model)
        {
            var viewModel = new ScienceDetailsPageViewModel()
            {
                Preamble = model.Preamble,
                Sections = GetSectionViewModels(model.MainContent),
                ImageTextSection = GetImageTextSectionViewModels(model.ImageAndTextSection),
                PublishedDate = model.GetPublishedDateOrDefault(),
                ReviewedDate = GetReviewedDate(model.ReviewedDate),
                Categories = GetCategories(model)
            };

            return viewModel;
        }

        private IEnumerable<NavigationItemViewModel> GetCategories(ScienceDetailsPage model)
        {
            var categories = _sciencePageCategoriesProvider.GetCategories(model);

            if (ExistenceUtility.IsNullOrEmpty(categories))
            {
                return Enumerable.Empty<NavigationItemViewModel>();
            }

            return _navigationItemService.GetViewModels(categories);
        }

        private DateTime? GetReviewedDate(DateTime reviewDate)
        {
            if (reviewDate == default(DateTime))
            {
                return null;
            }

            return reviewDate;
        }

        private IEnumerable<ScienceDetailsSectionViewModel> GetSectionViewModels(IEnumerable<ScienceDetailsSectionBaseSchema> mainContent)
        {
            var viewModels = new List<ScienceDetailsSectionViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(mainContent))
            {
                return viewModels;
            }

            foreach (var section in mainContent)
            {
                ScienceDetailsSectionViewModel viewModel = null;

                switch (section)
                {
                    case ScienceDetailsSectionRichTextSchema richText:
                        viewModel = CreateRichTextSection(richText);
                        break;
                    case ScienceDetailsSectionImageGallerySchema imageGallery:
                        viewModel = CreateImageGallerySection(imageGallery);
                        break;
                    case ScienceDetailsSectionImageTextSchema imageRichText:
                        viewModel = CreateImageRichTextSection(imageRichText);
                        break;
                    case ScienceDetailsSectionImageCodeSchema imageCode:
                        viewModel = CreateImageCodeSection(imageCode);
                        break;
                    case ScienceDetailsSectionSliderSchema slider:
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
        private IEnumerable<ScienceDetailsSectionViewModel> GetImageTextSectionViewModels(IEnumerable<ScienceDetailsSectionBaseSchema> imageTextSection)
        {
            var viewModels = new List<ScienceDetailsSectionViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(imageTextSection))
            {
                return viewModels;
            }

            foreach (var section in imageTextSection)
            {
                ScienceDetailsSectionViewModel viewModel = null;

                switch (section)
                {
                    case ScienceDetailsIndividualSectionImageTextSchema imageRichText:
                        viewModel = CreateIndividualImageRichTextSection(imageRichText);
                        break;
                    case ScienceDetailsIndividualSectionImageCodeSchema imageCode:
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

        private IEnumerable<ScienceDetailsSubSectionViewModel> GetSubSectionViewModels(IEnumerable<IPublishedContent> subSections, string parentHtmlId)
        {
            var viewModels = new List<ScienceDetailsSubSectionViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(subSections))
            {
                return viewModels;
            }

            foreach (var section in subSections)
            {
                ScienceDetailsSubSectionViewModel viewModel = null;

                switch (section)
                {
                    case ScienceDetailsSubSectionRichTextSchema richText:
                        viewModel = CreateRichTextSubSection(richText, parentHtmlId);
                        break;
                    case ScienceDetailsSubSectionImageGallerySchema imageGallery:
                        viewModel = CreateImageGallerySubSection(imageGallery, parentHtmlId);
                        break;
                    case ScienceDetailsSubSectionImageRichTextSchema imageRichText:
                        viewModel = CreateImageRichTextSubSection(imageRichText, parentHtmlId);
                        break;
                    case ScienceDetailsSubSectionImageCodeSchema imageCode:
                        viewModel = CreateImageCodeSubSection(imageCode, parentHtmlId);
                        break;
                    case ScienceDetailsSubSectionSliderSchema slider:
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

        private TViewModel CreateSection<TViewModel>(ScienceDetailsSectionBaseSchema schema, string parentSectionHtmlId = null) where TViewModel : ScienceDetailsSectionViewModelBase, new()
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

        private string GetPartialViewName(ScienceDetailsSectionBaseSchema schema)
        {
            switch (schema.DocumentTypeAlias)
            {
                case ScienceDetailsSectionRichTextSchema.ModelTypeAlias:
                case ScienceDetailsSubSectionRichTextSchema.ModelTypeAlias:
                    return ScienceDetailsPartialViewNames.RichText;
                case ScienceDetailsSubSectionImageGallerySchema.ModelTypeAlias:
                case ScienceDetailsSectionImageGallerySchema.ModelTypeAlias:
                    return ScienceDetailsPartialViewNames.ImageGallery;
                case ScienceDetailsIndividualSectionImageTextSchema.ModelTypeAlias:
                case ScienceDetailsSectionImageTextSchema.ModelTypeAlias:
                case ScienceDetailsSubSectionImageRichTextSchema.ModelTypeAlias:
                    return ScienceDetailsPartialViewNames.ImageRichText;
                case ScienceDetailsIndividualSectionImageCodeSchema.ModelTypeAlias:
                case ScienceDetailsSectionImageCodeSchema.ModelTypeAlias:
                case ScienceDetailsSubSectionImageCodeSchema.ModelTypeAlias:
                    return ScienceDetailsPartialViewNames.ImageCode;
                case ScienceDetailsSectionSliderSchema.ModelTypeAlias:
                case ScienceDetailsSubSectionSliderSchema.ModelTypeAlias:
                    return ScienceDetailsPartialViewNames.Slider;
                default:
                    throw new NotSupportedException($"Document Type, {schema.DocumentTypeAlias}, is not currently supported.");
            }
        }

        private ScienceDetailsRichTextSectionViewModel CreateRichTextSection(ScienceDetailsSectionRichTextSchema schema)
        {
            var model = CreateSection<ScienceDetailsRichTextSectionViewModel>(schema);

            model.Content = schema.Content;

            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        private ScienceDetailsImageGallerySectionViewModel CreateImageGallerySection(ScienceDetailsSectionImageGallerySchema schema)
        {
            var model = CreateSection<ScienceDetailsImageGallerySectionViewModel>(schema);

            model.Images = CreateSectionImageGallery(schema.Images);

            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        private ScienceDetailsRichTextSubSectionViewModel CreateRichTextSubSection(ScienceDetailsSubSectionRichTextSchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ScienceDetailsRichTextSubSectionViewModel>(schema, parentSectionHtmlId);

            model.Content = schema.Content;

            return model;
        }

        private ScienceDetailsImageGallerySubSectionViewModel CreateImageGallerySubSection(ScienceDetailsSubSectionImageGallerySchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ScienceDetailsImageGallerySubSectionViewModel>(schema, parentSectionHtmlId);

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
                    TitleText = image.GetPropertyValue<string>("titleText"),
                };

                if (string.IsNullOrEmpty(image.Url) == false)
                {
                    viewModels.Add(viewModel);
                }
            }

            return viewModels;
        }

        private ScienceDetailsImageRichTextSectionViewModel CreateImageRichTextSection(ScienceDetailsSectionImageTextSchema schema)
        {
            var model = CreateSection<ScienceDetailsImageRichTextSectionViewModel>(schema);

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
        private ScienceDetailsImageRichTextSectionViewModel CreateIndividualImageRichTextSection(ScienceDetailsIndividualSectionImageTextSchema schema)
        {
            var model = CreateSection<ScienceDetailsImageRichTextSectionViewModel>(schema);

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

        private ScienceDetailsImageRichTextSubSectionViewModel CreateImageRichTextSubSection(ScienceDetailsSubSectionImageRichTextSchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ScienceDetailsImageRichTextSubSectionViewModel>(schema, parentSectionHtmlId);

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

        private ScienceDetailsImageCodeSectionViewModel CreateImageCodeSection(ScienceDetailsSectionImageCodeSchema schema)
        {
            var model = CreateSection<ScienceDetailsImageCodeSectionViewModel>(schema);

            model.Content = schema.Content;
            model.ImageCode = schema.ImageCode;
            model.ImagePosition = schema.GetPropertyValue<string>("imagePosition");
            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }
        private ScienceDetailsImageCodeSectionViewModel CreateIndividualImageCodeSection(ScienceDetailsIndividualSectionImageCodeSchema schema)
        {
            var model = CreateSection<ScienceDetailsImageCodeSectionViewModel>(schema);

            model.Content = schema.Content;
            model.ImageCode = schema.ImageCode;
            model.ImagePosition = schema.GetPropertyValue<string>("imagePosition");
            model.SubSections = GetSubSectionViewModels(schema.SubSections, model.HtmlId);

            return model;
        }

        private ScienceDetailsImageCodeSubSectionViewModel CreateImageCodeSubSection(ScienceDetailsSubSectionImageCodeSchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ScienceDetailsImageCodeSubSectionViewModel>(schema, parentSectionHtmlId);

            model.Content = schema.Content;
            model.ImageCode = schema.ImageCode;
            model.ImagePosition = schema.GetPropertyValue<string>("imagePosition");

            return model;
        }

        private ScienceDetailsSliderSectionViewModel CreateSliderSection(ScienceDetailsSectionSliderSchema schema)
        {
            var model = CreateSection<ScienceDetailsSliderSectionViewModel>(schema);

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
                model.ImageLink = new NavigationItemViewModel() { Url = null, Text = null, Target = null };
            }

            return model;
        }

        private ScienceDetailsSliderSubSectionViewModel CreateSliderSubSection(ScienceDetailsSubSectionSliderSchema schema, string parentSectionHtmlId)
        {
            var model = CreateSection<ScienceDetailsSliderSubSectionViewModel>(schema);

            model.Content = schema.Content;
            model.ShowBackground = schema.ShowGreyBackground;
            model.ShowTimelineArrows = schema.ShowTimelineArrows;
            model.SliderItems = GetSliderItemViewModels(schema.SliderItems);

            return model;
        }
    }
}
