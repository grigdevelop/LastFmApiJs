using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LastFmApiJsNet.Api;

namespace LastFmApiJsNet.Services
{
    public class Chart : Base
    {
        #region Constructor

        public Chart(Session session)
            : base(session)
        {
            this.Session = session;
        }

        #endregion // Constructor

        #region Methods

        public Artist[] GetHypedArtists(int page = 1, int limit = 50)
        {
            var p = new RequestParameters();
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("chart.getHypedArtists", p);
            var res = extract<ArtistArray>(req, "artists");

            return res.Artists;
        }

        public Artist[] GetTopArtists(int page = 1, int limit = 50)
        {
            var p = new RequestParameters();
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("chart.getTopArtists", p);
            var res = extract<ArtistArray>(req, "artists");

            return res.Artists;
        }

        public Track[] GetHypedTracks(int page = 1, int limit = 50)
        {
            var p = new RequestParameters();
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("chart.getHypedTracks", p);
            var res = extract<TrackArray>(req, "tracks");

            return res.Tracks;
        }

        public Track[] GetLovedTracks(int page = 1, int limit = 50)
        {
            var p = new RequestParameters();
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("chart.getLovedTracks", p);
            var res = extract<TrackArray>(req, "tracks");

            return res.Tracks;
        }


        public Track[] GetTopTracks(int page = 1, int limit = 50)
        {
            var p = new RequestParameters();
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("chart.getTopTracks", p);
            var res = extract<TrackArray>(req, "tracks");

            return res.Tracks;
        }

        public Tag[] GetTopTags(int page = 1, int limit = 50)
        {
            var p = new RequestParameters();
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("chart.getTopTags", p);
            var res = extract<TagArray>(req, "tags");

            return res.Tags;
        }


        #endregion

        #region Utilities

        internal override RequestParameters getParams()
        {
            return new RequestParameters();
        }

        #endregion

    }
}
