using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using JNCC.PublicWebsite.Core.Utilities;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Controllers.SurfaceControllers
{
    public sealed class RelatedItemsSurfaceController : CoreSurfaceController
    {
        private readonly NavigationItemService _navigationItemService;
        private readonly RelatedItemsService _relatedItemsService;

        public RelatedItemsSurfaceController()
        {
            _navigationItemService = new NavigationItemService();
            _relatedItemsService = new RelatedItemsService(_navigationItemService);
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
