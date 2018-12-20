using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class NewsAndInsightsLandingFilteringViewModel : FilteringViewModel
    {
        public IReadOnlyDictionary<string, bool> ArticleTypes { get; set; }
        public IReadOnlyDictionary<string, bool> Years { get; set; }
        public IReadOnlyDictionary<string, bool> Teams { get; set; }
    }
}
