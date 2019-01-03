using JNCC.PublicWebsite.Core.Utilities;
using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class FilterGroupViewModel
    {
        public string Title { get; set; }
        public string Group { get; set; }
        public IReadOnlyDictionary<string, bool> Values { get; set; }
        public bool HasValues { get { return ExistenceUtility.IsNullOrEmpty(Values) == false; } }
    }
}
