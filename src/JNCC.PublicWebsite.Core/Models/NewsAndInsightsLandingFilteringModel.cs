﻿namespace JNCC.PublicWebsite.Core.Models
{
    public sealed class NewsAndInsightsLandingFilteringModel : FilteringModel
    {
        public string[] ArticleTypes { get; set; }
        public string[] Teams { get; set; }
        public int[] Years { get; set; }
    }
}
