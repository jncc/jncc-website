using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.ViewModels;
using RJP.MultiUrlPicker.Models;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class NavigationItemService
    {
        public IEnumerable<NavigationItemViewModel> GetViewModels(IEnumerable<Link> links)
        {
            return GetViewModels<NavigationItemViewModel>(links);
        }

        public IEnumerable<T> GetViewModels<T>(IEnumerable<Link> links) where T : NavigationItemViewModel, new()
        {
            var viewModels = new List<T>();

            if (links == null || links.Any() == false)
            {
                return viewModels;
            }

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
            if (link == null)
            {
                return default(T);
            }

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

            if (links == null || links.Any() == false)
            {
                return viewModels;
            }

            foreach (var link in links)
            {
                var viewModel = GetViewModel<T>(link);

                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public T GetViewModel<T>(RelatedLink link) where T : NavigationItemViewModel, new()
        {
            if (link == null)
            {
                return default(T);
            }

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

        public T GetViewModel<T>(IPublishedContent content) where T : NavigationItemViewModel, new()
        {
            if (content == null)
            {
                return default(T);
            }

            return new T()
            {
                Text = content.Name,
                Url = content.Url
            };
        }

        public IEnumerable<T> GetViewModels<T>(IEnumerable<IPublishedContent> contents) where T : NavigationItemViewModel, new()
        {
            var viewModels = new List<T>();

            if (contents == null || contents.Any() == false)
            {
                return viewModels;
            }

            foreach (var content in contents)
            {
                var viewModel = GetViewModel<T>(content);

                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public NavigationItemViewModel GetViewModel(IPublishedContent content)
        {
            return GetViewModel<NavigationItemViewModel>(content);
        }

        public IEnumerable<NavigationItemViewModel> GetViewModels(IEnumerable<IPublishedContent> contents)
        {
            return GetViewModels<NavigationItemViewModel>(contents);
        }
    }
}
