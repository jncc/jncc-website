using Umbraco.ModelsBuilder;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Models
{
    public partial class NewsAndInsightsLandingPage
    {
        ///<summary>
        /// Articles Per Page: Determines how many articles should be shown per page by default.
        ///</summary>
        [ImplementPropertyType("articlesPerPage")]
        public int ArticlesPerPage
        {
            get { return this.GetPropertyValue<int>("articlesPerPage"); }
        }
    }
}
