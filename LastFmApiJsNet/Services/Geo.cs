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
    public class Geo : Base
    {
        #region Methods

        public Event[] GetEvents(double? lng = null, double? lat = null, string location = null,
            double? distance = null, string tag = null,
            bool? festivalsonly = null, int page = 1, int limit = 50)
        {
            var p = getParams();
            if ( lng.HasValue && lat.HasValue )
            {
                p["long"] = lng.Value.ToString(CultureInfo.InvariantCulture);
                p["lat "] = lat.Value.ToString(CultureInfo.InvariantCulture);
            }
            if ( !string.IsNullOrWhiteSpace(location) )
                p["location"] = location;
            if ( distance.HasValue )
                p["distance"] = distance.Value.ToString(CultureInfo.InvariantCulture);
            if ( !string.IsNullOrWhiteSpace(tag) )
                p["tag"] = tag;
            if ( festivalsonly.HasValue )
                p["festivalsonly"] = festivalsonly.Value ? "0" : "1";
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("geo.getEvents", p);
            var res = extract<EventArray>(req, "events");

            return res.Events;
        }

        /// <summary>
        /// Get a chart of artists for a metro
        /// </summary>
        /// <param name="metro">The metro's name</param>
        /// <param name="country">A country name, as defined by the ISO 3166-1 country names standard</param>        
        /// <param name="weeklyRange">Timestamp of the weekly range</param>
        /// <param name="page">The page number to fetch. Defaults to first page</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50</param>
        /// <returns></returns>
        public Artist[] GetMetroArtistChart(string metro, string country,
            WeeklyRange weeklyRange = null, int page = 1, int limit = 50)
        {
            var p = getParams();
            p["metro"] = metro;
            p["country"] = country;
            if (weeklyRange != null)
            {
                p["start"] = Utilities.DateTimeToUTCTimestamp(weeklyRange.Start).ToString(CultureInfo.InvariantCulture);
                p["end"] = Utilities.DateTimeToUTCTimestamp(weeklyRange.End).ToString(CultureInfo.InvariantCulture);
            }
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("geo.getMetroArtistChart", p);
            var res = extract<ArtistArray>(req, "topartists");

            return res.Artists;
        }

        /// <summary>
        /// Get a chart of hyped (up and coming) artists for a metro
        /// </summary>
        /// <param name="metro">The metro's name</param>
        /// <param name="country">A country name, as defined by the ISO 3166-1 country names standard</param>        
        /// <param name="weeklyRange">Timestamp of the weekly range</param>
        /// <param name="page">The page number to fetch. Defaults to first page</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50</param>
        /// <returns></returns>
        public Artist[] GetMetroHypeArtistChart(string metro, string country,
            WeeklyRange weeklyRange = null, int page = 1, int limit = 50)
        {
            var p = getParams();
            p["metro"] = metro;
            p["country"] = country;
            if ( weeklyRange != null )
            {
                p["start"] = Utilities.DateTimeToUTCTimestamp(weeklyRange.Start).ToString(CultureInfo.InvariantCulture);
                p["end"] = Utilities.DateTimeToUTCTimestamp(weeklyRange.End).ToString(CultureInfo.InvariantCulture);
            }
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("geo.getMetroHypeArtistChart", p);
            var res = extract<ArtistArray>(req, "topartists");

            return res.Artists;
        }

        /// <summary>
        /// Get a chart of tracks for a metro
        /// </summary>
        /// <param name="metro">The metro's name</param>
        /// <param name="country">A country name, as defined by the ISO 3166-1 country names standard</param>        
        /// <param name="weeklyRange">Timestamp of the weekly range </param>
        /// <param name="page">The page number to fetch. Defaults to first page</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50</param>
        /// <returns>Chart of tracks for a metro</returns>
        public Track[] GetMetroHypeTrackChart(string metro, string country,
            WeeklyRange weeklyRange = null, int page = 1, int limit = 50)
        {
            var p = getParams();
            p["metro"] = metro;
            p["country"] = country;
            if ( weeklyRange != null )
            {
                p["start"] = Utilities.DateTimeToUTCTimestamp(weeklyRange.Start).ToString(CultureInfo.InvariantCulture);
                p["end"] = Utilities.DateTimeToUTCTimestamp(weeklyRange.End).ToString(CultureInfo.InvariantCulture);
            }
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("geo.getMetroHypeTrackChart", p);
            var res = extract<TrackArray>(req, "toptracks");

            return res.Tracks;
        }

        /// <summary>
        /// Get a chart of tracks for a metro
        /// </summary>
        /// <param name="metro">The metro's name</param>
        /// <param name="country">A country name, as defined by the ISO 3166-1 country names standard</param>        
        /// <param name="weeklyRange">Timestamp of the weekly range </param>
        /// <param name="page">The page number to fetch. Defaults to first page</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50</param>
        /// <returns>Chart of tracks for a metro</returns>
        public Track[] GetMetroTrackChart(string metro, string country,
            WeeklyRange weeklyRange = null, int page = 1, int limit = 50)
        {
            var p = getParams();
            p["metro"] = metro;
            p["country"] = country;
            if ( weeklyRange != null )
            {
                p["start"] = Utilities.DateTimeToUTCTimestamp(weeklyRange.Start).ToString(CultureInfo.InvariantCulture);
                p["end"] = Utilities.DateTimeToUTCTimestamp(weeklyRange.End).ToString(CultureInfo.InvariantCulture);
            }
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("geo.getMetroTrackChart", p);
            var res = extract<TrackArray>(req, "toptracks");

            return res.Tracks;
        }

        /// <summary>
        /// Get a chart of the artists which make that metro unique
        /// </summary>
        /// <param name="metro">The metro's name</param>
        /// <param name="country">A country name, as defined by the ISO 3166-1 country names standard</param>        
        /// <param name="weeklyRange">Timestamp of the weekly range </param>
        /// <param name="page">The page number to fetch. Defaults to first page</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50</param>
        /// <returns>Chart of the artists which make that metro unique</returns>
        public Artist[] GetMetroUniqueArtistChart(string metro, string country,
            WeeklyRange weeklyRange = null, int page = 1, int limit = 50)
        {
            var p = getParams();
            p["metro"] = metro;
            p["country"] = country;
            if ( weeklyRange != null )
            {
                p["start"] = Utilities.DateTimeToUTCTimestamp(weeklyRange.Start).ToString(CultureInfo.InvariantCulture);
                p["end"] = Utilities.DateTimeToUTCTimestamp(weeklyRange.End).ToString(CultureInfo.InvariantCulture);
            }
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("geo.getMetroUniqueArtistChart", p);
            var res = extract<ArtistArray>(req, "topartists");

            return res.Artists;
        }

        /// <summary>
        /// Get a chart of tracks for a metro
        /// </summary>
        /// <param name="metro">The metro's name</param>
        /// <param name="country">A country name, as defined by the ISO 3166-1 country names standard</param>        
        /// <param name="weeklyRange">Timestamp of the weekly range </param>
        /// <param name="page">The page number to fetch. Defaults to first page</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50</param>
        /// <returns>Chart of tracks for a metro</returns>
        public Track[] GetMetroUniqueTrackChart(string metro, string country,
            WeeklyRange weeklyRange = null, int page = 1, int limit = 50)
        {
            var p = getParams();
            p["metro"] = metro;
            p["country"] = country;
            if ( weeklyRange != null )
            {
                p["start"] = Utilities.DateTimeToUTCTimestamp(weeklyRange.Start).ToString(CultureInfo.InvariantCulture);
                p["end"] = Utilities.DateTimeToUTCTimestamp(weeklyRange.End).ToString(CultureInfo.InvariantCulture);
            }
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("geo.getMetroUniqueTrackChart", p);
            var res = extract<TrackArray>(req, "toptracks");

            return res.Tracks;
        }

        /// <summary>
        /// Get a list of available chart periods for this metro, expressed as date ranges which
        ///  can be sent to the chart services.
        /// </summary>
        /// <param name="metro">The metro name to fetch the charts list for.</param>
        /// <returns>List of available chart periods for this metro, expressed as date ranges which</returns>
        public WeeklyRange[] GetMetroWeeklyChartlist(string metro)
        {
            var p = getParams();
            p["metro"] = metro;

            var req = request("geo.getMetroWeeklyChartlist", p);
            var res = extract<WeeklyRangeArray>(req, "weeklychartlist");

            return res.WeeklyRanges;
        }

        /// <summary>
        /// Get a list of valid countries and metros for use in the other webservices
        /// </summary>
        /// <param name="country">Optionally restrict the results to those Metros from a particular country,
        ///  as defined by the ISO 3166-1 country names standard</param>
        /// <returns>List of valid countries and metros for use in the other webservices</returns>
        public Metro[] GetMetros(string country = null)
        {
            var p = getParams();
            if (!string.IsNullOrWhiteSpace(country))
                p["country"] = country;

            var req = request("geo.getMetros", p);
            var res = extract<MetroArray>(req, "metros");

            return res.Metros;
        }

        /// <summary>
        /// Get the most popular artists on Last.fm by country
        /// </summary>
        /// <param name="country">A country name, as defined by the ISO 3166-1 country names standard</param>
        /// <param name="page">The page number to fetch. Defaults to first page</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50</param>
        /// <returns>Most popular artists on Last.fm by country</returns>
        public Artist[] GetTopArtists(string country, int page = 1, int limit = 50)
        {
            var p = getParams();
            p["country"] = country;
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("geo.getTopArtists", p);
            var res = extract<ArtistArray>(req, "topartists");

            return res.Artists;
        }

        /// <summary>
        /// Get the most popular tracks on Last.fm last week by country
        /// </summary>
        /// <param name="country">A country name, as defined by the ISO 3166-1 country names standard</param>
        /// <param name="page">The page number to fetch. Defaults to first page</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50</param>
        /// <returns>Most popular tracks on Last.fm last week by country</returns>
        public Track[] GetTopTracks(string country, int page = 1, int limit = 50)
        {
            var p = getParams();
            p["country"] = country;
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("geo.getTopTracks", p);
            var res = extract<TrackArray>(req, "toptracks");

            return res.Tracks;
        }

        #endregion // Methods

        #region Constructor

        [JsonConstructor]
        public Geo(Session session)
            : base(session)
        {
            Session = session;
        }

        #endregion // Constructor


        #region Utilities

        internal override RequestParameters getParams()
        {
            return new RequestParameters();
        }

        #endregion // Utilities
    }
}
