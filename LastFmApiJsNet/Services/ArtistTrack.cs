using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LastFmApiJsNet.Api;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class ArtistTrack : Track
    {
        #region Constructors

        [JsonConstructor]
        public ArtistTrack(string artistName, string title, Session session)
            : base(artistName, title, session)
        {
            
        }

        public ArtistTrack(Artist artist, string title, Session session)
            : base(artist, title, session)
        {

        }

        #endregion // Constructors

        #region Fields

        [JsonProperty("date")]
        public Date Date { get; private set; }

        #endregion
    }

    public class ArtistTrackArray
    {
        [JsonConverter(typeof(JsonToArrayConverter<ArtistTrack>))]
        [JsonProperty("track")]
        public ArtistTrack[] Tracks { get; set; }
    }
}
