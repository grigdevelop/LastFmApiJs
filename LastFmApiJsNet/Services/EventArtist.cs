using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LastFmApiJsNet.Api;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class EventArtist
    {
        [JsonConverter(typeof(JsonToArrayConverter<Artist>))]
        [JsonProperty(PropertyName = "artist")]        
        public Artist[] Artists { get; private set; }

        [JsonProperty(PropertyName = "headliner")]
        public Artist HeadLiner { get; private set; }
    }
}
