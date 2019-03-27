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

            var pickedRelatedItemViewModels = GetPickedRelatedItemViewModels(composition.RelatedItems);
            viewModels.AddRange(pickedRelatedItemViewModels);

            return viewModels;
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

        private RelatedItemViewModel GetViewModel(IPublishedContent item)
        {
            var viewModel = new RelatedItemViewModel()
            {
                Content = _seoMetaDataService.GetSeoDescription(item),
                Link = new NavigationItemViewModel()
                {
                    Url = item.Url,
                    Text = "Read More"
                }
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
