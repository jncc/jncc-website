using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using SEOChecker.MVC;
using System;
using System.Collections.Specialized;
using System.Linq;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class GenericListingPageService : ListingService<GenericListingPage, IPublishedContent, GenericListingPageItemViewModel, FilteringModel>
    {
        public override NameValueCollection ConvertFiltersToNameValueCollection(FilteringModel filteringModel)
        {
            return new NameValueCollection();
        }

        public GenericListingPageViewModel GetViewModel(GenericListingPage model, FilteringModel filteringModel)
        {
            return new GenericListingPageViewModel()
            {
                Preamble = model.Preamble,
                Items = GetViewModels(model, filteringModel),
                PostListingContent = model.PostListingContent,
                Filters = ConvertFiltersToNameValueCollection(filteringModel)
            };
        }

        protected override int GetItemsPerPage(GenericListingPage parent)
        {
            return parent.ItemsPerPage;
        }

        protected override IOrderedEnumerable<IPublishedContent> GetOrderedChildren(GenericListingPage parent, FilteringModel filteringModel)
        {
            return parent.Children.OrderBy(x => x.SortOrder);
        }

        protected override GenericListingPageItemViewModel ToViewModel(IPublishedContent content)
        {
            var viewModel = new GenericListingPageItemViewModel()
            {
                Title = content.Name,
                Url = content.Url,
                Content = GetViewModelContent(content)
            };

            return viewModel;
        }

        private string GetViewModelContent(IPublishedContent model)
        {

            string content = null;

            if (model is ISeoComposition == false)
            {
                return content;
            }

            try
            {
                var seoMetaData = new MetaData(model.Id);
                content = seoMetaData.Description;
            }
            catch (Exception ex)
            {
                var message = string.Format("Unable to access SEO data for node ID {0}", model.Id);
                LogHelper.Error<GenericListingPage>(message, ex);
            }

            return content;
        }
    }
}
