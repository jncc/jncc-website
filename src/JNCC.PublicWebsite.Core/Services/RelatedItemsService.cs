using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    public sealed class RelatedItemsService
    {
        private NavigationItemService _navigationItemService;
        private const int _maxNumberOfRelatedItems = 3;

        public RelatedItemsService(NavigationItemService navigationItemService)
        {
            _navigationItemService = navigationItemService;
        }

        public IEnumerable<RelatedItemViewModel> GetViewModels(IRelatedItemsComposition composition)
        {
            var viewModels = new List<RelatedItemViewModel>();
            var manuallyAuthoredViewModels = GetViewModels(composition.RelatedItemsManuallyAuthoredItems);

            if (manuallyAuthoredViewModels.Any())
            {
                viewModels.AddRange(manuallyAuthoredViewModels);
            }

            var currentNumberOfRelatedItems = viewModels.Count();

            if (currentNumberOfRelatedItems == _maxNumberOfRelatedItems || string.IsNullOrWhiteSpace(composition.RelatedItemsDataHubQuery))
            {
                return viewModels;
            }

            var numberOfItemsRequiredFromDataHub = _maxNumberOfRelatedItems - currentNumberOfRelatedItems;
            var dataHubQueryViewModels = GetViewModels(composition.RelatedItemsDataHubQuery, numberOfItemsRequiredFromDataHub);

            if (dataHubQueryViewModels.Any())
            {
                viewModels.AddRange(dataHubQueryViewModels);
            }

            return viewModels;
        }

        private IEnumerable<RelatedItemViewModel> GetViewModels(string relatedItemsDataHubQuery, int numberOfItemsRequiredFromDataHub)
        {
            var viewModels = new List<RelatedItemViewModel>();

            return viewModels;
        }

        private IEnumerable<RelatedItemViewModel> GetViewModels(IEnumerable<IPublishedContent> relatedItemsManuallyAuthoredItems)
        {
            var viewModels = new List<RelatedItemViewModel>();
            if (relatedItemsManuallyAuthoredItems == null)
            {
                return viewModels;
            }

            var typedItems = relatedItemsManuallyAuthoredItems.OfType<RelatedItemSchema>();
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
