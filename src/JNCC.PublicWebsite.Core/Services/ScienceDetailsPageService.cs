using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using JNCC.PublicWebsite.Core.Providers;
using System;
using System.Collections.Generic;
using Umbraco.Core;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class ScienceDetailsPageService
    {
        private readonly ISciencePageCategoriesProvider _sciencePageCategoriesProvider;
        public ScienceDetailsPageService(ISciencePageCategoriesProvider sciencePageCategoriesProvider)
        {
            _sciencePageCategoriesProvider = sciencePageCategoriesProvider;
        }


        public ScienceDetailsPageViewModel GetViewModel(ScienceDetailsPage model)
        {
            var viewModel = new ScienceDetailsPageViewModel()
            {
                Preamble = model.Preamble,
                Sections = GetSectionViewModels(model.MainContent),
                PublishedDate = model.PublishedDate,
                ReviewedDate = GetReviewedDate(model.ReviewedDate)
            };            

            return viewModel;
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
