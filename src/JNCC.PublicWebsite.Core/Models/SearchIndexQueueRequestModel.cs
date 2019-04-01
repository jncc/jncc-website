using Newtonsoft.Json;

namespace JNCC.PublicWebsite.Core.Models
{
    internal sealed class SearchIndexQueueRequestModel
    {
        [JsonProperty("verb")]
        public string Verb { get; set; }
        [JsonProperty("index")]
        public string Index { get; set; }
        [JsonProperty("document")]
        public SearchIndexDocumentModel Document { get; set; }
    }
}
