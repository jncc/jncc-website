using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.Models
{
    internal sealed class SearchModel
    {
        [JsonProperty("hits")]
        public Hits Hits { get; set; }
    }

    internal sealed class Source
    {
        [JsonProperty("site")]
        public string Site { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("content_truncated")]
        public string Content { get; set; }
        //public List<Keyword> keywords { get; set; }
        [JsonProperty("published_date")]
        public string PublishedDate { get; set; }
        [JsonProperty("data_type")]
        public string DataType { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("file_extension")]
        public string FileExtension { get; set; }
        //public List<DatahubKeyword> datahub_keywords { get; set; }
    }

    internal sealed class Hit
    {
        //public string _index { get; set; }
        //public string _type { get; set; }
        [JsonProperty("_id")]
        public string Id { get; set; }
        //public double _score { get; set; }
        [JsonProperty("_source")]
        public Source Source { get; set; }
    }

    internal sealed class Hits
    {
        [JsonProperty("total")]
        public int Total { get; set; }
        //public double max_score { get; set; }
        [JsonProperty("hits")]
        public List<Hit> Results { get; set; }
    }

    internal sealed class Keyword
    {
        public string vocab { get; set; }
        public string value { get; set; }
    }

    internal sealed class DatahubKeyword
    {
        public string vocab { get; set; }
        public string value { get; set; }
    }
}
