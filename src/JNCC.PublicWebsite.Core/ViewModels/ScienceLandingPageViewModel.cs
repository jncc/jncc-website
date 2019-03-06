using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceLandingPageViewModel : IHasCalloutCardsViewModel
    {
        public IEnumerable<CalloutCardViewModel> CalloutCards { get; set; }
    }
}
