using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class RelatedItemsService
    {
        private readonly NavigationItemService _navigationItemService;

        public RelatedItemsService(NavigationItemService navigationItemService)
        {
            _navigationItemService = navigationItemService;
        }

        public IEnumerable<RelatedItemViewModel> GetViewModels(IRelatedItemsComposition composition)
        {
            var viewModels = new List<RelatedItemViewModel>();
            var relatedItems = composition.RelatedItems;

            if (relatedItems == null)
            {
                return viewModels;
            }

            var typedItems = relatedItems.OfType<RelatedItemSchema>();
            if (typedItems.Any() == false)
            {
                return viewModels;
            }

            foreach (var item in typedItems)
            {
                var viewModel = new RelatedItemViewModel()
                {
                    Content = item.Content,
                    Link = _navigationItemService.GetViewModel(item.Link)
                };

                if (item.Image != null)
                {
                    viewModel.ImageUrl = item.Image.Url;
                }

                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}
