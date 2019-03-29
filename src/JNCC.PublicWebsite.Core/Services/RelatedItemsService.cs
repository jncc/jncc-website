using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Web;
using System.Linq;
using System;
using JNCC.PublicWebsite.Core.Constants;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class RelatedItemsService
    {
        private readonly ISearchQueryService _searchQueryService;
        private readonly SeoMetaDataService _seoMetaDataService;
        private readonly UmbracoHelper _umbracoHelper;
        private const int MaximumRelatedItems = 3;
        private const int MaximumNumberOfSearchResults = 6;

        public RelatedItemsService(SeoMetaDataService seoMetaDataService, ISearchQueryService searchQueryService, UmbracoHelper umbracoHelper)
        {
            _searchQueryService = searchQueryService;
            _seoMetaDataService = seoMetaDataService;
            _umbracoHelper = umbracoHelper;
        }

        public IEnumerable<RelatedItemViewModel> GetViewModels(IRelatedItemsComposition composition, HomePage homePage)
        {
            var viewModels = new List<RelatedItemViewModel>();

            if (composition.ShowRelatedItems == false)
            {
                return viewModels;
            }
            var pickedRelatedItemViewModels = GetPickedRelatedItemViewModels(composition.RelatedItems, homePage);
            viewModels.AddRange(pickedRelatedItemViewModels);

            if (viewModels.Count < MaximumRelatedItems)
            {
                var excludedNodeIds = GetNodeIdsToExcludeFromSearchResults(composition);
                var searchQuery = GetSearchQuery(composition);
                var numberOfItemsToTake = MaximumRelatedItems - viewModels.Count();
                var searchedRelatedItemsViewModels = GetSearchQueryRelatedItemViewModels(searchQuery, MaximumNumberOfSearchResults, numberOfItemsToTake, excludedNodeIds, homePage);

                viewModels.AddRange(searchedRelatedItemsViewModels);
            }

            return viewModels;
        }

        private IEnumerable<string> GetNodeIdsToExcludeFromSearchResults(IRelatedItemsComposition composition)
        {
            var nodeIds = new List<string>
            {
                composition.Id.ToString()
            };

            if (ExistenceUtility.IsNullOrEmpty(composition.RelatedItems))
            {
                return nodeIds;
            }

            var pickedRelatedItemIds = composition.RelatedItems.Select(x => x.Id.ToString());
            nodeIds.AddRange(pickedRelatedItemIds);

            return nodeIds;
        }

        private string GetSearchQuery(IRelatedItemsComposition composition)
        {
            var searchTerm = composition.RelatedItemsSearchQuery;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return composition.GetHeadline();
            }

            return searchTerm;
        }

        private IEnumerable<RelatedItemViewModel> GetPickedRelatedItemViewModels(IEnumerable<IPublishedContent> relatedItems, HomePage homePage)
        {
            var viewModels = new List<RelatedItemViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(relatedItems))
            {
                return viewModels;
            }

            foreach (var item in relatedItems)
            {
                var viewModel = GetViewModel(item, homePage);

                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        private IEnumerable<RelatedItemViewModel> GetSearchQueryRelatedItemViewModels(string searchQuery, int numberOfItemsToSearchFor, int numberOfItemsToTake, IEnumerable<string> excludedNodeIds, HomePage homePage)
        {
            var viewModels = new List<RelatedItemViewModel>();
            var searchResults = _searchQueryService.Query(searchQuery, numberOfItemsToSearchFor, 0, SearchIndexingSites.Website);
            var filteredSearchResults = searchResults.Hits.Results.Where(x => excludedNodeIds.Contains(x.Id) == false)
                                                                  .Take(numberOfItemsToTake)
                                                                  .ToList();

            foreach (var result in filteredSearchResults)
            {
                var content = _umbracoHelper.TypedContent(result.Id);

                if (content != null)
                {
                    var viewModel = GetViewModel(content, homePage);

                    viewModels.Add(viewModel);
                }
            }

            return viewModels;
        }

        private RelatedItemViewModel GetViewModel(IPublishedContent item, HomePage homePage)
        {
            var viewModel = new RelatedItemViewModel()
            {
                Content = _seoMetaDataService.GetSeoDescription(item),
                Title = item.GetHeadline(),
                Url = item.Url
            };

            if (item is IPageHeroComposition)
            {
                var hero = (item as IPageHeroComposition);
                var heroImage = hero.HeroImage;

                if (heroImage != null)
                {
                    viewModel.ImageUrl = hero.HeroImage.GetCropUrl(Constants.ImageCropAliases.ListingThumbnail);
                }
            }

            if (string.IsNullOrWhiteSpace(viewModel.ImageUrl) && homePage.RelatedItemsFallbackImage != null)
            {
                viewModel.ImageUrl = homePage.RelatedItemsFallbackImage.GetCropUrl(Constants.ImageCropAliases.ListingThumbnail);
            }

            return viewModel;
        }
    }
}
