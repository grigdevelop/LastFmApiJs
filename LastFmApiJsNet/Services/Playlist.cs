using System;
using LastFmApiJsNet.Api;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class Playlist : Base, IEquatable<Playlist>
    {
        #region Members

        [JsonProperty("id")]
        public int ID { get; private set; }

        public User User { get; private set; }

        #endregion // Members

        #region Constructors

        [JsonConstructor]
        public Playlist(string username, int id, Session session)
            : base(session)
        {
            this.ID = id;
            this.User = new User(username, session);
        }

        public Playlist(User user, int id, Session session)
            : base(session)
        {
            this.ID = id;
            this.User = user;
        }

        #endregion // Constructors

        #region Methods

        /// <summary>
        /// Add a track to a Last.fm user's playlist
        /// </summary>
        /// <param name="track">The track name to add to the playlist.</param>
        /// <param name="artist">The artist name that corresponds to the track to be added.</param>
        public void AddTrack(string track, string artist)
        {
            requireAuthentication();

            var p = getParams();
            p["track"] = track;
            p["artist"] = artist;

            request("playlist.addTrack", p);
        }

        /// <summary>
        /// Create a Last.fm playlist on behalf of a user
        /// </summary>
        /// <param name="title">Title for the playlist</param>
        /// <param name="description"> Description for the playlist</param>
        public void Create(string title = null, string description = null)
        {
            requireAuthentication();

            var p = getParams();
            if (!string.IsNullOrWhiteSpace(title))
                p["title"] = title;
            if (!string.IsNullOrWhiteSpace(description))
                p["description"] = description;

            request("playlist.create", p);
        }

        #endregion

        #region Utilities

        internal override RequestParameters getParams()
        {
            RequestParameters p = new RequestParameters();
            p["playlistID"] = ID.ToString();

            return p;
        }

        #endregion // Utilities

        #region IEquatable<Playlist> Members

        public bool Equals(Playlist playlist)
        {
            return ( this.ID == playlist.ID );
        }

        #endregion
    }
}
