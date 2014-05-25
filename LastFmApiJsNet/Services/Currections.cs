using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LastFmApiJsNet.Api;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class ArtistCurrection
    {
        [JsonProperty(PropertyName = "artist")]
        public Artist Artist { get; set; }
    }

    public class ArtistCurrectionArray
    {
        [JsonConverter(typeof(JsonToArrayConverter<ArtistCurrection>))]
        [JsonProperty("correction")]
        public ArtistCurrection[] Currections { get; set; }
    }

    public class TrackCurrection
    {
        [JsonProperty(PropertyName = "track")]
        public Track Track { get; set; }
    }

    public class TrackCurrectionArray
    {
        [JsonConverter(typeof(JsonToArrayConverter<TrackCurrection>))]
        [JsonProperty("correction")]
        public TrackCurrection[] Currections { get; set; }
    }
}
