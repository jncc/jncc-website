﻿using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using Umbraco.Core.Models;

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

            foreach (var item in relatedItems)
            {
                var viewModel = GetViewModel(item);

                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        private RelatedItemViewModel GetViewModel(IPublishedContent item)
        {
            var viewModel = new RelatedItemViewModel()
            {
                Link = new NavigationItemViewModel()
                {
                    Url = item.Url,
                    Text = "Read More"
                }
            };

            if (item is ISeoComposition)
            {
                viewModel.Content = _seoMetaDataService.GetSeoDescription(item);
            }

            return viewModel;
        }
    }
}
