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
    public class Library : Base
    {
        #region Constructors

        [JsonConstructor]
        public Library(string username, Session session)
            : base(session)
        {
            this.User = new User(username, session);
        }

        public Library(User user, Session session)
            : base(session)
        {
            this.User = user;
        }

        #endregion // Constructor       

        #region Methods

        /// <summary>
        /// Add an album or collection of albums to a user's Last.fm library
        /// </summary>
        /// <param name="album"></param>
        public void AddAlbum(Album album)
        {
            requireAuthentication();
           
            var p = getParams();

            p["artist"] = album.Artist.Name;
            p["album"] = album.Name;
            
            request("library.addAlbum", p);
        }

        /// <summary>
        /// Add an artist to a user's Last.fm library
        /// </summary>
        /// <param name="artist"></param>
        public void AddArtist(Artist artist)
        {
            requireAuthentication();

            RequestParameters p = getParams();

            p["artist"] = artist.Name;

            request("library.addArtist", p);
        }

        /// <summary>
        /// Add a track to a user's Last.fm library
        /// </summary>
        /// <param name="track"></param>
        public void AddTrack(Track track)
        {
            requireAuthentication();

            RequestParameters p = getParams();

            p["artist"] = track.Artist.Name;
            p["track"] = track.Name;

            request("library.addTrack", p);
        }

        /// <summary>
        /// A paginated list of all the albums in a user's library, with play counts and tag counts.
        /// </summary>
        /// <param name="artist">An artist by which to filter tracks</param>
        /// <param name="page">The page number you wish to scan to.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <returns>List of all the albums in a user's library, with play counts and tag counts.</returns>
        public Album[] GetAlbums(string artist = null, int page = 1, int limit = 50)
        {
            var p = getParams();
            if (!string.IsNullOrWhiteSpace(artist))
                p["artist"] = artist;
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("library.getAlbums", p);
            var res = extract<AlbumArray>(req, "albums");

            return res.Albums;
        }

        /// <summary>
        /// A paginated list of all the artists in a user's library, with play counts and tag counts.
        /// </summary>
        /// <param name="page">The page number you wish to scan to.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <returns>List of all the artists in a user's library, with play counts and tag counts.</returns>
        public Artist[] GetArtists(int page = 1, int limit = 50)
        {
            var p = getParams();
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("library.getArtists", p);
            var res = extract<ArtistArray>(req, "artists");

            return res.Artists;
        }

        /// <summary>
        /// A paginated list of all the tracks in a user's library, with play counts and tag counts.
        /// </summary>
        /// <param name="album">An album by which to filter tracks (needs an artist)</param>
        /// <param name="page">The page number you wish to scan to.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <param name="artist">An artist by which to filter tracks</param>
        /// <returns>List of all the tracks in a user's library, with play counts and tag counts.</returns>
        public Track[] GetTracks(string artist = null, string album = null, int page = 1, int limit = 50)
        {
            var p = getParams();
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("library.getTracks", p);
            var res = extract<TrackArray>(req, "tracks");

            return res.Tracks;
        }

        /// <summary>
        /// Remove an album from a user's Last.fm library
        /// </summary>
        /// <param name="artist">The artist that composed the album</param>
        /// <param name="album">The name of the album you wish to remove</param>
        public void RemoveAlbum(string artist, string album)
        {
            requireAuthentication();

            var p = getParams();

            request("library.removeAlbum", p);
        }

        /// <summary>
        /// Remove an artist from a user's Last.fm library
        /// </summary>
        /// <param name="artist">The artist that composed the album</param>      
        public void RemoveArtist(string artist)
        {
            requireAuthentication();

            var p = getParams();

            request("library.removeArtist", p);
        }

        /// <summary>
        /// Remove a scrobble from a user's Last.fm library
        /// </summary>
        /// <param name="artist">The artist that composed the track</param>
        /// <param name="track">The name of the track</param>
        /// <param name="timestamp">The unix timestamp of the scrobble that you wish to remove</param>
        public void RemoveScrobble(string artist, string track, DateTime timestamp)
        {
            requireAuthentication();

            var p = getParams();
            p["artist"] = artist;
            p["track"] = track;
            p["timestamp"] = Utilities.DateTimeToUTCTimestamp(timestamp).ToString(CultureInfo.InvariantCulture);

            request("library.removeScrobble", p);
        }

        /// <summary>
        /// Remove a track from a user's Last.fm library
        /// </summary>
        /// <param name="artist">The artist that composed the track</param>
        /// <param name="track">The name of the track that you wish to remove</param>
        public void RemoveArtist(string artist, string track)
        {
            requireAuthentication();

            var p = getParams();
            p["artist"] = artist;
            p["track"] = track;

            request("library.removeTrack", p);
        }

        #endregion

        #region Members

        public User User { get; private set; }

        #endregion

        #region Utilities

        internal override RequestParameters getParams()
        {
            var p = new RequestParameters();
            p["user"] = User.Name;

            return p;
        }

        #endregion // Utilities
    }
}
