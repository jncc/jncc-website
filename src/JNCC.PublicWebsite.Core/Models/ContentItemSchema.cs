using JNCC.PublicWebsite.Core.Extensions;
using Umbraco.ModelsBuilder;

namespace JNCC.PublicWebsite.Core.Models
{
    public partial class ContentItemSchema
    {
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
