using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class ArtistBio
    {
        [JsonProperty(PropertyName = "published")]
        public DateTime Published { get; private set; }

        [JsonProperty(PropertyName = "summary")]
        public string Summary { get; private set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; private set; }

        [JsonProperty(PropertyName = "placeformed")]
        public string PlaceFormed { get; private set; }

        [JsonProperty(PropertyName = "yearformed")]
        public int YearFormed { get; private set; }

        [JsonProperty(PropertyName = "formationlist")]
        public FormationArray FormationList { get; private set; }
    }
}
