using JNCC.PublicWebsite.Core.ViewModels;
using RJP.MultiUrlPicker.Models;
using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.Services
{
    public sealed class NavigationItemService
    {
        public IEnumerable<NavigationItemViewModel> GetViewModels(IEnumerable<Link> links)
        {
            return GetViewModels<NavigationItemViewModel>(links);
        }

        public IEnumerable<T> GetViewModels<T>(IEnumerable<Link> links) where T : NavigationItemViewModel, new()
        {
            var viewModels = new List<T>();

            foreach (var link in links)
            {
                var viewModel = new T()
                {
                    Url = link.Url,
                    Text = link.Name,
                    Target = link.Target
                };

                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}
