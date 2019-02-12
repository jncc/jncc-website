using Umbraco.ModelsBuilder;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Models
{
    public partial class Image
    {
        ///<summary>
        /// Size
        ///</summary>
        [ImplementPropertyType("umbracoBytes")]
        public int UmbracoBytes
        {
            get { return this.GetPropertyValue<int>("umbracoBytes"); }
        }

        ///<summary>
        /// Height
        ///</summary>
        [ImplementPropertyType("umbracoHeight")]
        public int UmbracoHeight
        {
            get { return this.GetPropertyValue<int>("umbracoHeight"); }
        }

        ///<summary>
        /// Width
        ///</summary>
        [ImplementPropertyType("umbracoWidth")]
        public int UmbracoWidth
        {
            get { return this.GetPropertyValue<int>("umbracoWidth"); }
        }
    }
}
