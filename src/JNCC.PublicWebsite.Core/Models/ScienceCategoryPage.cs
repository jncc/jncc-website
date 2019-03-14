using JNCC.PublicWebsite.Core.Extensions;
using System.Collections.Generic;
using Umbraco.ModelsBuilder;

namespace JNCC.PublicWebsite.Core.Models
{
    public partial class ScienceCategoryPage : IScienceCategorisablePage
    {
        ///<summary>
        /// Related Categories: Allows editors to related categories to the current category.  This is a one way process, if the linked category is also related to this one, it must be authored on the other category page too.
        ///</summary>
        [ImplementPropertyType("relatedCategories")]
        public IEnumerable<ScienceCategoryPage> RelatedCategories
        {
            get { return this.GetPropertyValueOfTypeOrDefault<ScienceCategoryPage>("relatedCategories"); }
        }
    }
}
