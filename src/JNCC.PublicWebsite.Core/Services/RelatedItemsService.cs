using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class RelatedItemsService
    {
        private readonly NavigationItemService _navigationItemService;
        private readonly SeoMetaDataService _seoMetaDataService;

        public RelatedItemsService(NavigationItemService navigationItemService, SeoMetaDataService seoMetaDataService)
        {
            _navigationItemService = navigationItemService;
            _seoMetaDataService = seoMetaDataService;
        }

        public IEnumerable<RelatedItemViewModel> GetViewModels(IRelatedItemsComposition composition)
        {
            var viewModels = new List<RelatedItemViewModel>();

            if (composition.ShowRelatedItems == false)
            {
                return viewModels;
            }

            var relatedItems = composition.RelatedItems;

            if (ExistenceUtility.IsNullOrEmpty(relatedItems))
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
