using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class ScienceAtoZPageService
    {
        public ScienceAtoZPageService()
        {
        }

        public ScienceAtoZPageViewModel GetViewModel(ScienceAtoZpage model)
        {
            return new ScienceAtoZPageViewModel()
            {
                Headline = model.GetHeadline(),
                Preamble = model.Preamble,
                CategorisedPages = GetCategorisedPages(model)
            };
        }

        private IReadOnlyDictionary<char, IEnumerable<NavigationItemViewModel>> GetCategorisedPages(ScienceAtoZpage model)
        {
            var scienceLandingPage = model.Parent<ScienceLandingPage>();

            if (scienceLandingPage == null)
            {
                return null;
            }

            var categorisablePages = scienceLandingPage.Children<IScienceCategorisablePage>();
            if (ExistenceUtility.IsNullOrEmpty(categorisablePages))
            {
                return null;
            }

            return categorisablePages.CategorisePages();
        }
    }
}
