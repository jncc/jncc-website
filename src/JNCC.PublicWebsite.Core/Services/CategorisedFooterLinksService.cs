using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class CategorisedFooterLinksService
    {
        private readonly NavigationItemService _navigationItemService;

        public CategorisedFooterLinksService(NavigationItemService navigationItemService)
        {
            _navigationItemService = navigationItemService;
        }

        public IEnumerable<CategorisedFooterLinksViewModel> GetViewModels(IEnumerable<CategorisedFooterLinksSchema> schemas)
        {
            var viewModels = new List<CategorisedFooterLinksViewModel>();

            if (schemas == null)
            {
                return viewModels;
            }

            foreach (var schema in schemas)
            {
                var categoryViewModel = new CategorisedFooterLinksViewModel()
                {
                    Heading = schema.Heading,
                    Links = _navigationItemService.GetViewModels(schema.Links)
                };

                viewModels.Add(categoryViewModel);
            }

            return viewModels;
        }
    }
}
