using Umbraco.ModelsBuilder;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Models
{
    public partial class GenericListingPage
    {
        ///<summary>
        /// Items Per Page: Determines how many pages should be shown per page by default.
        ///</summary>
        [ImplementPropertyType("itemsPerPage")]
        public int ItemsPerPage
        {
            get { return this.GetPropertyValue<int>("itemsPerPage"); }
        }
    }
}
