namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class SeoMetaDataViewModel
    {
        public string Title { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }

        public bool NoIndex { get; set; }
    }
}
