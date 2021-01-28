using System.Collections.Generic;
using Newtonsoft.Json;

namespace AlyaDiscord
{

    public partial class SccObjectMovie
    {
        public SccObjectServices services { get; set; }
        public List<SccObjectItem> items { get; set; }
        public bool IsConcert { get; set; }
    }

    public partial class SccObjectItem
    {
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public long? season { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public long? episode { get; set; }
        public List<string> streams { get; set; }
    }

    public partial class SccObjectServices
    {
        #nullable enable
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public string? tmdb { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public string? trakt { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public string? csfd { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public string? imdb { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public string? tvdb { get; set; }
    }
}
