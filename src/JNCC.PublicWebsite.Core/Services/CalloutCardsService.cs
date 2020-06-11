using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class CalloutCardsService
    {
        private readonly NavigationItemService _navigationItemService;

        public CalloutCardsService(NavigationItemService navigationItemService)
        {
            _navigationItemService = navigationItemService;
        }

        public IEnumerable<CalloutCardViewModel> GetCalloutCards(IEnumerable<CalloutCardSchema> cards)
        {
            var viewModels = new List<CalloutCardViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(cards))
            {
                return viewModels;
            }

            foreach (var card in cards)
            {
                var viewModel = new CalloutCardViewModel()
                {
                    Title = card.Title,
                    Content = card.Content,
                    ReadMoreButton = _navigationItemService.GetViewModel(card.ReadMoreButton)
                };

                if (card.Image.Image != null)
                {
                    viewModel.Image = new ImageViewModel()
                    {
                        Url = card.Image.Image.Url,
                        AlternativeText = card.Image.Image.GetPropertyValue<string>("altText").IsNullOrWhiteSpace() ? card.Image.Image.GetPropertyValue<string>("altText") : card.Image.Image.Name,
                        TitleText = card.Image.Image.GetPropertyValue<string>("titleText"),
                    };
                };

                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}
