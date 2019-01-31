using JNCC.PublicWebsite.Core.Models;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace JNCC.PublicWebsite.Core.Services
{
    public sealed class ImagePickerApiService
    {
        private readonly UmbracoHelper _umbracoHelper;

        public ImagePickerApiService(UmbracoHelper umbracoHelper)
        {
            _umbracoHelper = umbracoHelper;
        }

        public bool TryFindRoot(int id, out IPublishedContent root)
        {
            root = _umbracoHelper.TypedMedia(id);

            if (root == null)
            {
                return false;
            }

            return root is Folder;
        }
    }
}
