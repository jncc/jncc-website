using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    internal class ManuallyAuthoredRelatedItemsService : IManuallyAuthoredRelatedItemsService
    {
        private readonly NavigationItemService _navigationItemService;

        public ManuallyAuthoredRelatedItemsService(NavigationItemService navigationItemService)
        {
            _navigationItemService = navigationItemService;
        }

        public IEnumerable<RelatedItemViewModel> GetViewModels(IEnumerable<IPublishedContent> manuallyAuthoredItems)
        {
            var viewModels = new List<RelatedItemViewModel>();
            if (manuallyAuthoredItems == null)
            {
                return viewModels;
            }

            var typedItems = manuallyAuthoredItems.OfType<RelatedItemSchema>();
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