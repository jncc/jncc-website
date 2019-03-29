using JNCC.PublicWebsite.Core.Converters;
using Newtonsoft.Json;
using System;

namespace JNCC.PublicWebsite.Core.Models
{
    internal sealed class SearchIndexDocumentModel
    {
        [JsonProperty("id")]
        public int NodeId { get; set; }

        [JsonProperty("site")]
        public string Site { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("published_date")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime Published { get; set; }

        [JsonProperty("file_base64")]
        public string FileBase64Encoded { get; set; }

        [JsonProperty("file_extension")]
        public string FileExtension { get; set; }

        [JsonProperty("file_bytes")]
        public long FileSizeInBytes { get; set; }
    }
}
