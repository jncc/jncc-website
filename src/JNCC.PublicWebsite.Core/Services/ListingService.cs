using System.Linq;
using Umbraco.Core.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    internal abstract class ListingService<TParent, TChild, TViewModel> where TParent : IPublishedContent
                                                                        where TChild : IPublishedContent
    {
        public Paged​Result<TViewModel> GetViewModels(TParent parent, int pageNumber)
        {
            var children = GetOrderedChildren(parent);
            var numberOfItemsPerPage = GetItemsPerPage(parent);
            var results = new Paged​Result<TViewModel>(children.LongCount(), pageNumber, numberOfItemsPerPage);

            var viewModels = children.Skip(results.GetSkipSize())
                                     .Take(numberOfItemsPerPage)
                                     .Select(ToViewModel);

            results.Items = viewModels;

            return results;
        }

        protected abstract int GetItemsPerPage(TParent parent);

        protected abstract IOrderedEnumerable<TChild> GetOrderedChildren(TParent parent);

        protected abstract TViewModel ToViewModel(TChild content);
    }
}