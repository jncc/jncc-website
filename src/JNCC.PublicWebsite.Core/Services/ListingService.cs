using JNCC.PublicWebsite.Core.Models;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Umbraco.Core.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    internal abstract class ListingService<TParent, TChild, TViewModel, TListFilteringModel> where TParent : IPublishedContent
                                                                                             where TChild : IPublishedContent
                                                                                             where TListFilteringModel : ListFilteringModel
    {
        public Paged​Result<TViewModel> GetViewModels(TParent parent, TListFilteringModel filteringModel)
        {
            var children = GetOrderedChildren(parent, filteringModel);
            var numberOfItemsPerPage = GetItemsPerPage(parent);
            var results = new Paged​Result<TViewModel>(children.LongCount(), filteringModel.PageNumber, numberOfItemsPerPage);

            var viewModels = children.Skip(results.GetSkipSize())
                                     .Take(numberOfItemsPerPage)
                                     .Select(ToViewModel);

            results.Items = viewModels;

            return results;
        }

        public abstract NameValueCollection ConvertFiltersToNameValueCollection(TListFilteringModel filteringModel);

        protected abstract int GetItemsPerPage(TParent parent);

        protected abstract IOrderedEnumerable<TChild> GetOrderedChildren(TParent parent, TListFilteringModel filteringModel);

        protected abstract TViewModel ToViewModel(TChild content);



    }
}