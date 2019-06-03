using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public class PageAttributesViewModel
    {
        public string HTMLLangRef { get; set; }
        public bool NoIndex { get; set; }
        public CultureInfo LTRValue { get; internal set; }
    }
}
