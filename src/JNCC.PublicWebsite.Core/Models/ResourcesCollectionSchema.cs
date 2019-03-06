using JNCC.PublicWebsite.Core.Extensions;
using System.Collections.Generic;
using Umbraco.ModelsBuilder;

namespace JNCC.PublicWebsite.Core.Models
{
    public partial class ResourcesCollectionSchema
    {
        ///<summary>
        /// Resources: The key resources for this collection.
        ///</summary>
        [ImplementPropertyType("resources")]
        public IEnumerable<CalloutCardSchema> Resources
        {
            get { return this.GetPropertyValueOfTypeOrDefault<CalloutCardSchema>("resources"); }
        }
    }
}
