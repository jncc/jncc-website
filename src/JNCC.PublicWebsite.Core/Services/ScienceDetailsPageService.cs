using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using JNCC.PublicWebsite.Core.Providers;
using System;
using System.Collections.Generic;
using Umbraco.Core;
using JNCC.PublicWebsite.Core.Utilities;
using System.Linq;

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
                PublishedDate = model.PublishedDate,
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

        private IEnumerable<ScienceDetailsSectionViewModel> GetSectionViewModels(IEnumerable<ScienceDetailsSectionBaseSchema> mainContent)
        {
            var viewModels = new List<ScienceDetailsSectionViewModel>();

            foreach (var section in mainContent)
            {
                ScienceDetailsSectionViewModel viewModel = null;

                if (section is ScienceDetailsSectionRichTextSchema)
                {
                    viewModel = new ScienceDetailsRichTextSectionViewModel()
                    {
                        Content = (section as ScienceDetailsSectionRichTextSchema).Content
                    };
                }

                if (viewModel != null)
                {
                    viewModel.Headline = section.Headline;
                    viewModel.HtmlId = section.Headline.ToUrlSegment();
                    viewModels.Add(viewModel);
                }
            }

            return viewModels;
        }

        public ScienceSidebarViewModel GetSidebarViewModel(ScienceDetailsPage model)
        {
            return new ScienceSidebarViewModel
            {
                PrimaryCallToActionButton = _navigationItemService.GetViewModel(model.SidebarPrimaryCallToActionButton),
                Categories = GetCategoriesWithFeaturedPages(model),
                SeeAlsoLinks = _navigationItemService.GetViewModels(model.SidebarSeeAlsoLinks)
            };
        }

        private IEnumerable<MainNavigationItemViewModel> GetCategoriesWithFeaturedPages(ScienceDetailsPage model)
        {
            var categories = _sciencePageCategoriesProvider.GetCategories(model);

            var viewModels = new List<MainNavigationItemViewModel>();

            foreach(var category in categories)
            {
                var viewModel = _navigationItemService.GetViewModel<MainNavigationItemViewModel>(category);

                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        private DateTime? GetReviewedDate(DateTime reviewDate)
        {
            if (reviewDate == default(DateTime))
            {
                return null;
            }

            return reviewDate;
        }
    }
}
