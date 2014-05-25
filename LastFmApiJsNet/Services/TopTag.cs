using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LastFmApiJsNet.Api;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class TopTag
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }

    public class TopTagArray
    {
        [JsonConverter(typeof(JsonToArrayConverter<TopTag>))]
        [JsonProperty(PropertyName = "tag")]
        public TopTag[] TopTags { get; set; }
    }
}
