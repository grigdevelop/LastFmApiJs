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
    public class Tag : Base, IEquatable<Tag>
    {
        #region Members

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("url")]
        public string Url { get; private set; }

        [JsonProperty("reach")]
        public int Reach { get; private set; }

        [JsonProperty("taggings")]
        public int Taggings { get; private set; }

        [JsonProperty("streamable")]
        public int Stramable { get; private set; }

        [JsonProperty("wiki")]
        public Wiki Wiki { get; private set; }

        #endregion // Members

        #region Constructor

        public Tag(string name, Session session)
            : base(session)
        {
            Name = name;
        }

        #endregion // Constructor

        #region Methods

        /// <summary>
        /// Get the metadata for a tag
        /// </summary>
        /// <returns></returns>
        public Tag GetInfo()
        {
            var req = request("tag.getInfo");
            var res = extract<Tag>(req, "tag");

            return res;
        }

        /// <summary>
        /// Search for tags similar to this one. Returns tags ranked by similarity, based on listening data.
        /// </summary>
        /// <returns></returns>
        public Tag[] GetSimilar()
        {
            var req = request("tag.getSimilar");
            var res = extract<TagArray>(req, "similartags");

            return res.Tags;
        }

        /// <summary>
        /// Get the top albums tagged by this tag, ordered by tag count.
        /// </summary>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <returns></returns>
        public Album[] GetTopAlbums(int page = 1, int limit = 50)
        {
            var p = getParams();
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("tag.getTopAlbums", p);
            var res = extract<AlbumArray>(req, "topalbums");         

            return res.Albums;
        }

        /// <summary>
        /// Get the top artists tagged by this tag, ordered by tag count.
        /// </summary>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <returns></returns>
        public Artist[] GetTopArtists(int page = 1, int limit = 50)
        {
            var p = getParams();
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("tag.getTopArtists", p);
            var res = extract<ArtistArray>(req, "topartists");

            return res.Artists;
        }

        /// <summary>
        /// Fetches the top global tags on Last.fm, sorted by popularity (number of times used)
        /// </summary>
        /// <returns></returns>
        public Tag[] GetTopTags()
        {
            var req = request("tag.getTopTags");
            var res = extract<TagArray>(req, "toptags");

            return res.Tags;
        }

        /// <summary>
        /// Get the top tracks tagged by this tag, ordered by tag count.
        /// </summary>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <returns></returns>
        public Track[] GetTopTracks(int page = 1, int limit = 50)
        {
            var p = getParams();
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("tag.getTopTracks", p);
            var res = extract<TrackArray>(req, "toptracks");

            return res.Tracks;
        }

        /// <summary>
        /// Get an artist chart for a tag, for a given date range. 
        /// If no date range is supplied, it will return the most recent artist chart for this tag.
        /// </summary>
        /// <param name="weeklyRange">
        /// The date at which the chart should start from. See Tag.getWeeklyChartList for more.
        /// The date at which the chart should end on. See Tag.getWeeklyChartList for more.
        /// </param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <returns></returns>
        public Artist[] GetWeeklyArtistChart(WeeklyRange weeklyRange = null, int limit = 50)
        {
            var p = getParams();
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);
            if ( weeklyRange != null )
            {
                p["start"] = Utilities.DateTimeToUTCTimestamp(weeklyRange.Start).ToString(CultureInfo.InvariantCulture);
                p["end"] = Utilities.DateTimeToUTCTimestamp(weeklyRange.End).ToString(CultureInfo.InvariantCulture);
            }

            var req = request("tag.getWeeklyArtistChart", p);
            var res = extract<ArtistArray>(req, "weeklyartistchart");

            return res.Artists;
        }

        /// <summary>
        /// Get a list of available charts for this tag, expressed as date ranges which can be sent to the chart services.
        /// </summary>
        /// <returns></returns>
        public WeeklyRange[] GetWeeklyChartList()
        {
            var req = request("tag.getWeeklyChartList");
            var res = extract<WeeklyRangeArray>(req, "weeklychartlist");

            return res.WeeklyRanges;
        }

        /// <summary>
        /// Search for a tag by name. Returns matches sorted by relevance.
        /// </summary>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit"> The number of results to fetch per page. Defaults to 30.</param>
        /// <returns></returns>
        public TagSearchResults Search(int page = 1, int limit = 30)
        {
            var p = getParams();
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("tag.search", p);
            var res = extract<TagSearchResults>(req, "results");

            return res;
        }

        #endregion // Methods

        #region Utilities

        internal override RequestParameters getParams()
        {
            var p = new RequestParameters();
            p["tag"] = this.Name;

            return p;
        }

        /// <summary>
        /// String representation of the object.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/>
        /// </returns>
        public override string ToString()
        {
            return this.Name;
        }

        #endregion // Utilities

        #region IEquatable<Tag> Members

        public bool Equals(Tag other)
        {
            return this.Name == other.Name;
        }

        #endregion
    }

    internal class TagArray
    {
        [JsonConverter(typeof(JsonToArrayConverter<Tag>))]
        [JsonProperty(PropertyName = "tag")]
        public Tag[] Tags { get; set; }
    }
}
