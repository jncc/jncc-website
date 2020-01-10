using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class IFramePageService
    {
        private readonly NavigationItemService _navigationItemService;

        public IFramePageService(NavigationItemService navigationItemService)
        {
            _navigationItemService = navigationItemService;
        }

        public IFramePageViewModel GetViewModel(IFramePage model, Uri currentUrl)
        {
            return new IFramePageViewModel()
            {
                Navigation = GetNavigation(model),
                SourceUrl = GetSourceUrl(model, currentUrl),
                CookieError = model.CookieDisabledError
            };
        }

        private MainNavigationViewModel GetNavigation(IFramePage model)
        {
            return new MainNavigationViewModel()
            {
                Items = _navigationItemService.GetViewModels<MainNavigationItemViewModel>(model.Navigation),
                HasPageHero = false
            };
        }

        private string GetSourceUrl(IFramePage model, Uri currentUrl)
        {
            if (model.PassthroughQuerystringParameters == false)
            {
                return model.SourceUrl;
            }

            var sourceUrlBuilder = new UriBuilder(model.SourceUrl);
            var currentUrlBuilder = new UriBuilder(currentUrl);

            var sourceUrlQuery = HttpUtility.ParseQueryString(sourceUrlBuilder.Query);
            var currentUrlQuery = HttpUtility.ParseQueryString(currentUrlBuilder.Query);

            sourceUrlBuilder.Query = BuildNewQueryStringFavouringCurrentUrlQueryValues(sourceUrlQuery, currentUrlQuery);
            return sourceUrlBuilder.ToString();
        }

        private string BuildNewQueryStringFavouringCurrentUrlQueryValues(NameValueCollection sourceUrlQuery, NameValueCollection currentUrlQuery)
        {
            var allKeys = new List<string>();
            allKeys.AddRange(currentUrlQuery.AllKeys);
            allKeys.AddRange(sourceUrlQuery.AllKeys);

            var newQuery = new StringBuilder();

            foreach (var key in allKeys.Distinct())
            {
                var newValue = currentUrlQuery.Get(key);
                var existingValue = sourceUrlQuery.Get(key);
                var actualValue = string.Empty;

                if (string.IsNullOrWhiteSpace(newValue) == false)
                {
                    actualValue = newValue;
                }
                else if (string.IsNullOrWhiteSpace(existingValue) == false)
                {
                    actualValue = existingValue;
                }

                newQuery.AppendFormat("{0}={1}&", key, actualValue);
            }

            return newQuery.ToString().TrimEnd('&');
        }

    }
}
