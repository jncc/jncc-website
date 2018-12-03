using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Core.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class SeoMetaDataService
    {
        public SeoMetaDataViewModel GetViewModel(IPublishedContent content)
        {
            return new SeoMetaDataViewModel
            {
                Title = content.Name
            };
        }


        public SeoMetaDataViewModel GetViewModelFromSeoSettings(ISeoComposition composition)
        {
            var settings = composition.SeoSettings;

            if (settings == null)
            {
                return GetViewModel(composition);
            }

            return new SeoMetaDataViewModel
            {
                Title = settings.Title,
                Description = settings.Description,
                Keywords = settings.Keywords
            };
        }
    }
}
