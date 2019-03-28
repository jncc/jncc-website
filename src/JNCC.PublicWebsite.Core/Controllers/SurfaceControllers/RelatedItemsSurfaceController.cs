using JNCC.PublicWebsite.Core.Configuration;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using JNCC.PublicWebsite.Core.Utilities;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class RelatedItemsSurfaceController : CoreSurfaceController
    {
        private readonly ISearchQueryService _searchQueryService;
        private readonly RelatedItemsService _relatedItemsService;
        private readonly SeoMetaDataService _seoMetaDataService;

        public RelatedItemsSurfaceController()
        {
            _searchQueryService = new SearchService(SearchConfiguration.GetConfig());
            _seoMetaDataService = new SeoMetaDataService();
            _relatedItemsService = new RelatedItemsService(_seoMetaDataService, _searchQueryService, Umbraco);
        }

        [ChildActionOnly]
        public ActionResult RenderRelatedItems()
        {
            if (CurrentPage is IRelatedItemsComposition == false)
            {
                return EmptyResult();
            }

            var viewModels = _relatedItemsService.GetViewModels(CurrentPage as IRelatedItemsComposition);

            if (ExistenceUtility.IsNullOrEmpty(viewModels))
            {
                return EmptyResult();
            }

            return PartialView("~/Views/Partials/RelatedItems.cshtml", viewModels);
        }
    }
}
