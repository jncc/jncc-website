using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.Services
{
    public sealed class CategorisedFooterLinksService
    {
        public IEnumerable<CategorisedFooterLinksViewModel> GetViewModels(IEnumerable<CategorisedFooterLinksSchema> schemas)
        {
            var viewModels = new List<CategorisedFooterLinksViewModel>();

            if (schemas == null)
            {
                return viewModels;
            }

            foreach (var schema in schemas)
            {
                var links = new List<NavigationItemViewModel>();
                foreach(var link in schema.Links)
                {
                    var navigationItem = new NavigationItemViewModel()
                    {
                        Target = link.Target,
                        Url = link.Url,
                        Text = link.Name
                    };

                    links.Add(navigationItem);
                }

                var categoryViewModel = new CategorisedFooterLinksViewModel()
                {
                    Heading = schema.Heading,
                    Links = links
                };

                viewModels.Add(categoryViewModel);
            }

            return viewModels;
        }
    }
}
