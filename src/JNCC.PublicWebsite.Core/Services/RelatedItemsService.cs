using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class RelatedItemsService
    {
        private const int _maxNumberOfRelatedItems = 3;
        private readonly IManuallyAuthoredRelatedItemsService _manuallyAuthoredRelatedItemsService;
        private readonly IDataHubQueryRelatedItemsService _dataHubQueryRelatedItemsService;

        public RelatedItemsService(IManuallyAuthoredRelatedItemsService manuallyAuthoredRelatedItemsService = null, IDataHubQueryRelatedItemsService dataHubQueryRelatedItemsService = null)
        {
            _manuallyAuthoredRelatedItemsService = manuallyAuthoredRelatedItemsService;
            _dataHubQueryRelatedItemsService = dataHubQueryRelatedItemsService;
        }

        public IEnumerable<RelatedItemViewModel> GetViewModels(IRelatedItemsComposition composition)
        {
            var viewModels = new List<RelatedItemViewModel>();

            if (_manuallyAuthoredRelatedItemsService == null)
            {
                return viewModels;
            }

            var manuallyAuthoredViewModels = _manuallyAuthoredRelatedItemsService.GetViewModels(composition.RelatedItemsManuallyAuthoredItems);

            if (manuallyAuthoredViewModels.Any())
            {
                viewModels.AddRange(manuallyAuthoredViewModels);
            }

            var currentNumberOfRelatedItems = viewModels.Count();

            if (currentNumberOfRelatedItems == _maxNumberOfRelatedItems || _dataHubQueryRelatedItemsService == null)
            {
                return viewModels;
            }

            var numberOfItemsRequiredFromDataHub = _maxNumberOfRelatedItems - currentNumberOfRelatedItems;
            var dataHubQueryViewModels = _dataHubQueryRelatedItemsService.GetViewModels(composition.RelatedItemsDataHubQuery, numberOfItemsRequiredFromDataHub);

            if (dataHubQueryViewModels.Any())
            {
                viewModels.AddRange(dataHubQueryViewModels);
            }

            return viewModels;
        }
    }
}
