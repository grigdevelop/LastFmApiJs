using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LastFmApiJsNet.Api;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class User : Base, IEquatable<User>
    {
        #region Members

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("realname")]
        public string RealName { get; set; }

        #endregion

        #region Constructors

        [JsonConstructor]
        public User(string name, Session session)
            : base(session)
        {
            Name = name;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get a list of tracks by a given artist scrobbled by this user,
        ///  including scrobble time. Can be limited to specific timeranges, 
        /// defaults to all time.
        /// </summary>
        /// <param name="artist">The artist name you are interested in</param>
        /// <param name="startTimestamp">An unix timestamp to start at.</param>
        /// <param name="endTimestamp">An unix timestamp to end at.</param>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <returns></returns>
        public ArtistTrack[] GetArtistTracks(string artist, DateTime? startTimestamp = null,
            DateTime? endTimestamp = null,
            int page = 1 )
        {
            var p = getParams();
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["artist"] = artist;
            if (startTimestamp.HasValue)
                p["startTimestamp "] = Utilities.DateTimeToUTCTimestamp(startTimestamp.Value)
                    .ToString(CultureInfo.InvariantCulture);
            if (endTimestamp.HasValue)
                p["endTimestamp"] = Utilities.DateTimeToUTCTimestamp(endTimestamp.Value)
                    .ToString(CultureInfo.InvariantCulture);

            var req = request("user.getArtistTracks", p);
            var res = extract<ArtistTrackArray>(req, "artisttracks");

            return res.Tracks;
        }

        /// <summary>
        /// Returns the tracks banned by the user
        /// </summary>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <returns></returns>
        public Track[] GetBannedTracks(int page = 1, int limit = 50)
        {
            var p = getParams();
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("user.getBannedTracks", p);
            var res = extract<TrackArray>(req, "bannedtracks");

            return res.Tracks;
        }

        /// <summary>
        /// String representation of the object.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/>
        /// </returns>
        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region Utilities       

        internal override RequestParameters getParams()
        {
            var p = new RequestParameters();
            p["user"] = Name;

            return p;
        }

        #endregion

        #region IEquatable<User> Members

        public bool Equals(User user)
        {
            return ( user.Name == this.Name );
        }

        #endregion
    }

    public class UserArray
    {
        [JsonConverter(typeof(JsonToArrayConverter<User>))]
        [JsonProperty("user")]
        public User[] Users { get; private set; }
    }
}
