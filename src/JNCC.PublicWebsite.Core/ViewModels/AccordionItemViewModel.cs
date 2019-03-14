using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public class AccordionItemViewModel : IAccordionHeaderViewModel
    {
        public AccordionItemViewModel()
        {
        }

        public AccordionItemViewModel(string htmlId, string title, IHtmlString content) : this()
        {
            HtmlId = htmlId;
            Title = title;
            Content = content;
        }

        public string HtmlId { get; set; }
        public string Title { get; set; }
        public IHtmlString Content { get; set; }
    }

    public interface IAccordionHeaderViewModel
    {
        string HtmlId { get; set; }
        string Title { get; set; }
    }
}