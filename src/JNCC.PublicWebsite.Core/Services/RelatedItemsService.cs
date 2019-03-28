using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class RelatedItemsService
    {
        private readonly ISearchQueryService _searchQueryService;
        private readonly SeoMetaDataService _seoMetaDataService;
        private readonly UmbracoHelper _umbracoHelper;
        private const int MaximumRelatedItems = 3;

        public RelatedItemsService(SeoMetaDataService seoMetaDataService, ISearchQueryService searchQueryService, UmbracoHelper umbracoHelper)
        {
            _searchQueryService = searchQueryService;
            _seoMetaDataService = seoMetaDataService;
            _umbracoHelper = umbracoHelper;
        }

        public IEnumerable<RelatedItemViewModel> GetViewModels(IRelatedItemsComposition composition)
        {
            var viewModels = new List<RelatedItemViewModel>();

            if (composition.ShowRelatedItems == false)
            {
                return viewModels;
            }

            var pickedRelatedItemViewModels = GetPickedRelatedItemViewModels(composition.RelatedItems);
            viewModels.AddRange(pickedRelatedItemViewModels);

            if (viewModels.Count < MaximumRelatedItems)
            {
                var numberOfItemsToSearchFor = MaximumRelatedItems - viewModels.Count;
                var searchQuery = GetSearchQuery(composition);
                var searchedRelatedItemsViewModels = GetSearchQueryRelatedItemViewModels(searchQuery, numberOfItemsToSearchFor);

                viewModels.AddRange(searchedRelatedItemsViewModels);
            }

            return viewModels;
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

        private IEnumerable<RelatedItemViewModel> GetPickedRelatedItemViewModels(IEnumerable<IPublishedContent> relatedItems)
        {
            var viewModels = new List<RelatedItemViewModel>();

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

        private IEnumerable<RelatedItemViewModel> GetSearchQueryRelatedItemViewModels(string searchQuery, int numberOfItemsToSearchFor)
        {
            var viewModels = new List<RelatedItemViewModel>();

            return viewModels;
        }

        private RelatedItemViewModel GetViewModel(IPublishedContent item)
        {
            var viewModel = new RelatedItemViewModel()
            {
                Content = _seoMetaDataService.GetSeoDescription(item),
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

            return viewModel;
        }
    }
}
