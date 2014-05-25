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
    public class Group : Base, IEquatable<Group>
    {
        #region Members

        [JsonProperty("name")]
        public string Name { get; private set; }

        #endregion // Members

        #region Constructor

        [JsonConstructor]
        public Group(string groupName, Session session)
            : base(session)
        {
            Name = groupName;
        }

        #endregion // Constructor

        #region Methods

        /// <summary>
        /// Get the hype list for a group
        /// </summary>
        /// <returns></returns>
        public Artist[] GetHype()
        {
            var req = request("group.getHype");
            var res = extract<ArtistArray>(req, "weeklyartistchart");

            return res.Artists;
        }

        /// <summary>
        /// Get a list of members for this group
        /// </summary>
        /// <param name="page">The results page you would like to fetch</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50</param>
        /// <returns>List of members for this group</returns>
        public User[] GetMembers(int page = 1, int limit = 50)
        {
            var p = getParams();
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("group.getMembers", p);
            var res = extract<UserArray>(req, "members");

            return res.Users;
        }

        /// <summary>
        /// Get an album chart for a group, for a given date range. 
        /// If no date range is supplied, it will return the most recent album chart for this group.
        /// </summary>
        /// <param name="weeklyRange">
        /// The date at which the chart should start from.
        /// The date at which the chart should end on. 
        /// </param>
        /// <returns>Album chart for a group, for a given date range</returns>
        public Album[] GetWeeklyAlbumChart(WeeklyRange weeklyRange = null)
        {
            var p = getParams();
            if (weeklyRange != null)
            {
                p["from"] = Utilities.DateTimeToUTCTimestamp(weeklyRange.Start).ToString(CultureInfo.InvariantCulture);
                p["top"] = Utilities.DateTimeToUTCTimestamp(weeklyRange.End).ToString(CultureInfo.InvariantCulture);
            }

            var req = request("group.getWeeklyAlbumChart", p);
            var res = extract<AlbumArray>(req, "weeklyalbumchart");

            return res.Albums;
        }

        /// <summary>
        /// Get an artist chart for a group, for a given date range.
        /// If no date range is supplied, it will return the most recent album chart for this group.
        /// </summary>
        /// <param name="weeklyRange">
        /// The date at which the chart should start from.
        /// The date at which the chart should end on. 
        /// </param>
        /// <returns>Artist chart for a group, for a given date range</returns>
        public Artist[] GetWeeklyArtistChart(WeeklyRange weeklyRange = null)
        {
            var p = getParams();
            if ( weeklyRange != null )
            {
                p["from"] = Utilities.DateTimeToUTCTimestamp(weeklyRange.Start).ToString(CultureInfo.InvariantCulture);
                p["top"] = Utilities.DateTimeToUTCTimestamp(weeklyRange.End).ToString(CultureInfo.InvariantCulture);
            }

            var req = request("group.getWeeklyArtistChart", p);
            var res = extract<ArtistArray>(req, "weeklyartistchart");

            return res.Artists;
        }

        /// <summary>
        /// Get a list of available charts for this group, expressed as date ranges which can be sent to the chart services.
        /// </summary>
        /// <returns>List of available charts for this group</returns>
        public WeeklyRange[] GetWeeklyChartList()
        {
            var req = request("group.getWeeklyChartList");
            var res = extract<WeeklyRangeArray>(req, "weeklychartlist");

            return res.WeeklyRanges;
        }

        /// <summary>
        /// Get a track chart for a group, for a given date range. 
        /// If no date range is supplied, it will return the most recent album chart for this group.
        /// </summary>
        /// <param name="weeklyRange">
        /// The date at which the chart should start from.
        /// The date at which the chart should end on. 
        /// </param>
        /// <returns>Track chart for a group, for a given date range.</returns>
        public Track[] GetWeeklyTrackChart(WeeklyRange weeklyRange = null)
        {
            var p = getParams();
            if ( weeklyRange != null )
            {
                p["from"] = Utilities.DateTimeToUTCTimestamp(weeklyRange.Start).ToString(CultureInfo.InvariantCulture);
                p["top"] = Utilities.DateTimeToUTCTimestamp(weeklyRange.End).ToString(CultureInfo.InvariantCulture);
            }

            var req = request("group.getWeeklyTrackChart", p);
            var res = extract<TrackArray>(req, "weeklytrackchart");

            return res.Tracks;
        }

        #endregion // Methods

        #region Utilities

        internal override Api.RequestParameters getParams()
        {
            var p = new RequestParameters();
            p["group"] = Name;

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
            return Name;
        }

        #endregion        
    
        #region IEquatable<Group> Members

        public bool Equals(Group group)
        {
            return ( group.Name == this.Name );
        }       

        #endregion
    }
}
