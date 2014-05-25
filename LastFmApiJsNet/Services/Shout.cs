using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LastFmApiJsNet.Api;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class Shout
    {
        [JsonProperty("body")]
        public string Body { get; private set; }

        [JsonProperty("author")]
        public string Author { get; private set; }

        [JsonProperty("date")]
        public DateTime Date { get; private set; }
    }

    public class ShoutArray
    {
        [JsonConverter(typeof(JsonToArrayConverter<Shout>))]
        [JsonProperty("shout")]
        public Shout[] Shouts { get; private set; }
    }
}
