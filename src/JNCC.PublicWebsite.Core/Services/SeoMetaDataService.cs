using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using SEOChecker.MVC;
using System;
using Umbraco.Core.Logging;
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
            var Title = "";

            if (settings == null)
            {
                return GetViewModel(composition);
            }

            if(string.IsNullOrWhiteSpace(settings.Title) == false)
            {
                Title = settings.Title; 
            } else if (composition.Name.ToLower() == "home")
            {
                Title = "JNCC - Adviser to Government on Nature Conservation";
            } else
            {
                Title = composition.Name + " | JNCC - Adviser to Government on Nature Conservation";
            }

            return new SeoMetaDataViewModel
            {
                Title = Title,
                Description = settings.Description,
                Keywords = settings.Keywords
            };
        }

        public string GetSeoDescription(IPublishedContent content)
        {
            var description = string.Empty;

            if (content is ISeoComposition == false)
            {
                return description;
            }

            try
            {
                var seoMetaData = new MetaData(content.Id);
                description = seoMetaData.Description;
            }
            catch (Exception ex)
            {
                var message = string.Format("Unable to access SEO data for node ID {0}", content.Id);
                LogHelper.Error<GenericListingPage>(message, ex);
            }

            return description;
        }
    }
}
