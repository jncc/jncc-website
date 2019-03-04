﻿using JNCC.PublicWebsite.Core.Utilities;
using System.Collections.Generic;
using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceCategoryPageViewModel
    {
        public string Headline { get; set; }
        public IHtmlString Preamble { get; set; }
        public IReadOnlyDictionary<char, IEnumerable<NavigationItemViewModel>> CategorisedPages { get; set; }
        public bool HasCategorisedPages
        {
            get
            {
                return ExistenceUtility.IsNullOrEmpty(CategorisedPages) == false;
            }
        }
        public IReadOnlyDictionary<char, IEnumerable<NavigationItemViewModel>> RelatedCategories { get; set; }
        public bool HasRelatedCategories
        {
            get
            {
                return ExistenceUtility.IsNullOrEmpty(RelatedCategories) == false;
            }
        }
    }
}