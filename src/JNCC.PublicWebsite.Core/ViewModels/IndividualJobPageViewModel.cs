using System.Collections.Generic;
using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class IndividualJobPageViewModel
    {
        public IReadOnlyDictionary<string, string> KeyData { get; set; }
        public IEnumerable<AccordionItemViewModel> TabbedContent { get; set; }
    }
}
