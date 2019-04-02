using Newtonsoft.Json;

namespace JNCC.PublicWebsite.Core.Models
{
    public class SearchIndexDocumentKeywordModel
    {
        [JsonProperty("vocab")]
        public string Vocab { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}