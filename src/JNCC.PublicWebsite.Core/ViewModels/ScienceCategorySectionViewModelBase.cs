namespace JNCC.PublicWebsite.Core.ViewModels
{
    public abstract class ScienceCategorySectionViewModelBase
    {
        public string HtmlId { get; set; }
        public string Headline { get; set; }
        public string PartialViewName { get; set; }
        public bool HideHeadline { get; set; }
    }
}