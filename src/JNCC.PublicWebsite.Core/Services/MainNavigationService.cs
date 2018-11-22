using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    public sealed class MainNavigationService
    {
        private const int _maximumMenuLevel = 3;

        public IEnumerable<MainNavigationItemViewModel> GetRootMenuItems(IPublishedContent root)
        {
            var items = new List<MainNavigationItemViewModel>
            {
                ToMenuItem(root)
            };

            var childItems = GetMenuItems(root);


            if (childItems != null)
            {
                items.AddRange(childItems);
            }

            return items;
        }

        private IEnumerable<MainNavigationItemViewModel> GetMenuItems(IPublishedContent parent)
        {
            var menuItems = new List<MainNavigationItemViewModel>();

            if (parent.AreChildrenVisible() == false)
            {
                return menuItems;
            }

            foreach (var item in parent.VisibleChildren())
            {
                var menuItem = ToMenuItem(item);

                if (item.Level < _maximumMenuLevel)
                {
                    var childItems = GetMenuItems(item);

                    if (childItems != null && childItems.Any())
                    {
                        menuItem.Children = childItems;
                    }
                }

                menuItems.Add(menuItem);
            }

            return menuItems;
        }

        private MainNavigationItemViewModel ToMenuItem(IPublishedContent content)
        {
            return new MainNavigationItemViewModel()
            {
                Text = content.Name,
                Url = content.Url
            };
        }

    }
}
