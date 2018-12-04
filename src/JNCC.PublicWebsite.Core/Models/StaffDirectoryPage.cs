using Umbraco.ModelsBuilder;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Models
{
    public partial class StaffDirectoryPage
    {
        ///<summary>
        /// Profiles Per Page: Determines how many profiles should be shown per page by default.
        ///</summary>
        [ImplementPropertyType("profilesPerPage")]
        public int ProfilesPerPage
        {
            get { return this.GetPropertyValue<int>("profilesPerPage"); }
        }
    }
}
