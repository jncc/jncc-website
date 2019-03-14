using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public interface IHasCalloutCardsViewModel
    {
        IEnumerable<CalloutCardViewModel> CalloutCards { get; set; }
    }
}
