using JNCC.PublicWebsite.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceDetailsPageViewModel
    {
        public IHtmlString Preamble { get; set; }
        public IEnumerable<ScienceDetailsSectionViewModel> Sections { get; set; }
        public bool HasSections
        {
            get
            {
                return ExistenceUtility.IsNullOrEmpty(Sections) == false;
            }
        }

        public DateTime PublishedDate { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public IEnumerable<NavigationItemViewModel> Categories { get; set; }
        public bool HasCategories
        {
            get
            {
                return ExistenceUtility.IsNullOrEmpty(Categories) == false;
            }
        }
    }
}
