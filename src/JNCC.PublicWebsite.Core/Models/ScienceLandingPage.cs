using JNCC.PublicWebsite.Core.Extensions;
using System.Collections.Generic;
using Umbraco.ModelsBuilder;

namespace JNCC.PublicWebsite.Core.Models
{
    public partial class ScienceLandingPage
    {
        ///<summary>
        /// Callout Cards: Boxes for important information which will appear below the headline or hero content of the page.
        ///</summary>
        [ImplementPropertyType("calloutCards")]
        public IEnumerable<CalloutCardSchema> CalloutCards
        {
            get { return this.GetPropertyValueOfTypeOrDefault<CalloutCardSchema>("calloutCards"); }
        }
    }
}
