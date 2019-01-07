using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class NewsAndInsightsLandingFilteringViewModel : FilteringViewModel
    {
        public FilterGroupViewModel ArticleTypes { get; set; }
        public FilterGroupViewModel Years { get; set; }
    }
}
