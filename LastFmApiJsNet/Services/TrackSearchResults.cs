using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class TrackSearchResults : SearchResults<Track>
    {
        [JsonProperty("trackmatches")]
        public TrackArray TrackMatches { get; private set; }
    }
}
