using System.Collections.Specialized;
using System.Linq;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
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
                Url = content.Url
            };

            return viewModel;
        }
    }
}
