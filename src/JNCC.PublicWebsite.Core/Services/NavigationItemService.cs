using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.ViewModels;
using RJP.MultiUrlPicker.Models;
using System.Collections.Generic;
using Umbraco.Web.Models;

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
                var viewModel = GetViewModel<T>(link);
                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public NavigationItemViewModel GetViewModel(Link link)
        {
            return GetViewModel<NavigationItemViewModel>(link);
        }

        public T GetViewModel<T>(Link link) where T : NavigationItemViewModel, new()
        {
            return new T()
            {
                Url = link.Url,
                Text = link.Name,
                Target = link.Target
            };
        }

        public IEnumerable<T> GetViewModels<T>(IEnumerable<RelatedLink> links) where T : NavigationItemViewModel, new()
        {
            var viewModels = new List<T>();

            foreach (var link in links)
            {
                var viewModel = GetViewModel<T>(link);

                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public T GetViewModel<T>(RelatedLink link) where T : NavigationItemViewModel, new()
        {
            var viewModel = new T()
            {
                Url = link.Link,
                Text = link.Caption
            };

            if (link.NewWindow)
            {
                viewModel.Target = HtmlAnchorTargets.Blank;
            }

            return viewModel;
        }
    }
}
