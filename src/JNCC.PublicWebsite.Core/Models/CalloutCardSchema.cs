using JNCC.PublicWebsite.Core.Extensions;
using Umbraco.ModelsBuilder;

namespace JNCC.PublicWebsite.Core.Models
{
    public partial class CalloutCardSchema
    {       
        ///<summary>
        /// Image: An image which illustrates this content item.
        ///</summary>
        [ImplementPropertyType("image")]
        public ContentImageSchema Image
        {
            get
            {
                return this.GetPropertyValueFirstOfTypeOrDefault<ContentImageSchema>("image");
            }
        }

    }
}
