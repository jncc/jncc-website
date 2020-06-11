using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class LatestNewsSectionService
    {
        private const string XPathForLatestNewsItems = "//ArticlePage[@isDoc]";
        private const int NumberOfLatestNewsItems = 2;
        private readonly UmbracoHelper _umbracoHelper;

        public LatestNewsSectionService(UmbracoHelper umbracoHelper)
        {
            _umbracoHelper = umbracoHelper;
        }

        public LatestNewsSectionViewModel GetViewModel(HomePage model)
        {
            if (model == null)
            {
                return new LatestNewsSectionViewModel
                {
                    LatestNews = Enumerable.Empty<LatestNewsItemViewModel>(),
                    SocialFeed = new SocialFeedViewModel()
                };
            }

            return new LatestNewsSectionViewModel
            {
                LatestNews = GetLatestNewsItems(model),
                SocialFeed = GetSocialFeed(model)
            };
        }

        private SocialFeedViewModel GetSocialFeed(HomePage content)
        {
            var viewModel = new SocialFeedViewModel();

            if (content.ShowSocialFeed == false)
            {
                return viewModel;
            }

            viewModel.TwitterFeedUrl = content.TwitterFeedUrl;
            viewModel.NumberOfTweets = content.NumberOfTweets;

            return viewModel;
        }

        private IEnumerable<LatestNewsItemViewModel> GetLatestNewsItems(HomePage content)
        {
            var viewModels = new List<LatestNewsItemViewModel>();

            if (content.ShowLatestNews == false)
            {
                return viewModels;
            }

            var latestNewsItems = _umbracoHelper.TypedContentAtXPath(XPathForLatestNewsItems)
                                                .OfType<ArticlePage>()
                                                .OrderByDescending(x => x.PublishDate)
                                                .Take(NumberOfLatestNewsItems);

            if (ExistenceUtility.IsNullOrEmpty(latestNewsItems))
            {
                return viewModels;
            }

            foreach (var newsItem in latestNewsItems)
            {
                var viewModel = new LatestNewsItemViewModel
                {
                    Title = string.IsNullOrWhiteSpace(newsItem.Headline) ? newsItem.Name : newsItem.Headline,
                    PublishDate = newsItem.PublishDate,
                    Description = newsItem.LandingPageContent,
                    Url = newsItem.Url
                };

                if (newsItem.HeroImage != null)
                {
                    viewModel.ImageUrl = newsItem.HeroImage.GetCropUrl(ImageCropAliases.ListingThumbnail);
                    viewModel.ImageAltText = newsItem.HeroImage.GetPropertyValue<string>("altText").IsNullOrWhiteSpace() ? newsItem.Headline : newsItem.HeroImage.GetPropertyValue<string>("altText");
                    viewModel.ImageTitleText = newsItem.HeroImage.GetPropertyValue<string>("titleText");
                }

                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}
