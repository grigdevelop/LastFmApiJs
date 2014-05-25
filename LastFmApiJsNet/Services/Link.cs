using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LastFmApiJsNet.Api;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class Link
    {
        [JsonProperty(PropertyName = "#text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "rel")]
        public string Rel { get; set; }

        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }
    }

    public class LinkArray
    {
        [JsonConverter(typeof(JsonToArrayConverter<Link>))]
        [JsonProperty(PropertyName = "link")]
        public Link[] Links { get; set; }
    }
}
