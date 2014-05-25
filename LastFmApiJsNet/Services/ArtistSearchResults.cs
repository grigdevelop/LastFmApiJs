using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class ArtistSearchResults : SearchResults<Artist>
    {
        [JsonProperty("artistmatches")]
        public ArtistArray ArtistMatches { get; private set; }
    }
    
}
