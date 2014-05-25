using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LastFmApiJsNet.Api;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class Artist : Base, IEquatable<Artist>, IHasImage, IHasURL, IShareable, ITaggable
    {
        #region Setters

        [JsonProperty("#text")]
        private string NameSetter { set { this.Name = value; } }

        #endregion

        #region Members

        /// <summary>
        /// The name of the artist.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }

        /// <summary>
        /// The mbid of the artist.
        /// </summary>
        [JsonProperty(PropertyName = "mbid")]
        public string Mbid { get; private set; }

        [JsonProperty(PropertyName = "bio")]
        public ArtistBio Bio { get; private set; }

        #region IHasImage Members

        [JsonProperty(PropertyName = "image")]
        public Images Images { get; private set; }

        #endregion // IHasImage Members

        #region IHasURL Members
        /// <summary>
        /// Returns the Last.fm page of this object.
        /// </summary>
        /// <param name="language">
        /// A <see cref="SiteLanguage"/>
        /// </param>
        /// <returns>
        /// A <see cref="System.String"/>
        /// </returns>
        public string GetURL(SiteLanguage language)
        {
            string domain = getSiteDomain(language);

            return "http://" + domain + "/music/" + urlSafe(Name);
        }

        /// <summary>
        /// The object's Last.fm page url.
        /// </summary>
        public string URL
        { get { return GetURL(SiteLanguage.English); } }

        #endregion // IHasURL Members

        #region IShareable Members

        /// <summary>
        /// Share this artist with others.
        /// </summary>
        /// <param name="recipients">
        /// A <see cref="Recipients"/>
        /// </param>
        public void Share(Recipients recipients)
        {
            if ( recipients.Count > 1 )
            {
                foreach ( string recipient in recipients )
                {
                    Recipients r = new Recipients();
                    r.Add(recipient);
                    Share(r);
                }

                return;
            }

            requireAuthentication();

            RequestParameters p = getParams();
            p["recipient"] = recipients[0];

            request("artist.Share", p);
        }

        /// <summary>
        /// Share this artist with others.
        /// </summary>
        /// <param name="recipients">
        /// A <see cref="Recipients"/>
        /// </param>
        /// <param name="message">
        /// A <see cref="System.String"/>
        /// </param>
        public void Share(Recipients recipients, string message)
        {
            if ( recipients.Count > 1 )
            {
                foreach ( string recipient in recipients )
                {
                    Recipients r = new Recipients();
                    r.Add(recipient);
                    Share(r, message);
                }

                return;
            }

            requireAuthentication();

            RequestParameters p = getParams();
            p["recipient"] = recipients[0];
            p["message"] = message;

            request("artist.Share", p);
        }

        #endregion

        #region ITaggable Members

        /// <summary>
        /// Add one or more tags to this artist.
        /// </summary>
        /// <param name="tags">
        /// A <see cref="Tag"/>
        /// </param>
        public void AddTags(params Tag[] tags)
        {
            //This method requires authentication
            requireAuthentication();

            foreach ( Tag tag in tags )
            {
                RequestParameters p = getParams();
                p["tags"] = tag.Name;

                request("artist.addTags", p);
            }
        }

        /// <summary>
        /// Add one or more tags to this artist.
        /// </summary>
        /// <param name="tags">
        /// A <see cref="System.String"/>
        /// </param>
        public void AddTags(params string[] tags)
        {
            foreach ( string tag in tags )
                AddTags(new Tag(tag, Session));
        }

        /// <summary>
        /// Add one or more tags to this artist.
        /// </summary>
        /// <param name="tags">
        /// A <see cref="TagCollection"/>
        /// </param>
        public void AddTags(TagCollection tags)
        {
            foreach ( Tag tag in tags )
                AddTags(tag);
        }

        /// <summary>
        /// Returns the tags set by the authenticated user to this artist.
        /// </summary>
        /// <returns>
        /// A <see cref="Tag"/>
        /// </returns>
        public Tag[] GetTags()
        {
            //This method requires authentication
            requireAuthentication();

            var jdoc = request("artist.getTags");

            var tags = extract<TagArray>(jdoc, "tags");
            return tags.Tags;
        }

        /// <summary>
        /// Returns the top tags of this artist on Last.fm.
        /// </summary>
        /// <returns>
        /// A <see cref="TopTag"/>
        /// </returns>
        public TopTag[] GetTopTags()
        {
            var jDoc = request("artist.getTopTags");
            var list = extract<TopTagArray>(jDoc, "toptags");
            return list.TopTags;
        }

        /// <summary>
        /// Returns the top tags of this artist on Last.fm.
        /// </summary>
        /// <param name="limit">
        /// A <see cref="System.Int32"/>
        /// </param>
        /// <returns>
        /// A <see cref="TopTag"/>
        /// </returns>
        public TopTag[] GetTopTags(int limit)
        {
            return GetTopTags().Take(limit).ToArray();
        }

        /// <summary>
        /// Removes from the tags that the authenticated user has set to this artist.
        /// </summary>
        /// <param name="tags">
        /// A <see cref="System.String"/>
        /// </param>
        public void RemoveTags(params string[] tags)
        {
            //This method requires authentication
            requireAuthentication();

            foreach ( string tag in tags )
                RemoveTags(new Tag(tag, Session));
        }

        /// <summary>
        /// Removes from the tags that the authenticated user has set to this artist.
        /// </summary>
        /// <param name="tags">
        /// A <see cref="Tag"/>
        /// </param>
        public void RemoveTags(params Tag[] tags)
        {
            //This method requires authentication
            requireAuthentication();

            foreach ( Tag tag in tags )
            {
                RequestParameters p = getParams();
                p["tag"] = tag.Name;

                request("artist.removeTag", p);
            }
        }

        /// <summary>
        /// Removes from the tags that the authenticated user has set to this artist.
        /// </summary>
        /// <param name="tags">
        /// A <see cref="TagCollection"/>
        /// </param>
        public void RemoveTags(TagCollection tags)
        {
            foreach ( Tag tag in tags )
                RemoveTags(tag);
        }

        /// <summary>
        /// Sets the tags applied by the authenticated user to this artist to
        /// only those tags. Removing and adding tags as neccessary.
        /// </summary>
        /// <param name="tags">
        /// A <see cref="Tag"/>
        /// </param>
        public void SetTags(Tag[] tags)
        {
            List<Tag> newSet = new List<Tag>(tags);
            List<Tag> current = new List<Tag>(GetTags());
            List<Tag> toAdd = new List<Tag>();
            List<Tag> toRemove = new List<Tag>();

            foreach ( Tag tag in newSet )
                if ( !current.Contains(tag) )
                    toAdd.Add(tag);

            foreach ( Tag tag in current )
                if ( !newSet.Contains(tag) )
                    toRemove.Add(tag);

            if ( toAdd.Count > 0 )
                AddTags(toAdd.ToArray());
            if ( toRemove.Count > 0 )
                RemoveTags(toRemove.ToArray());
        }

        /// <summary>
        /// Sets the tags applied by the authenticated user to this artist to
        /// only those tags. Removing and adding tags as neccessary.
        /// </summary>
        /// <param name="tags">
        /// A <see cref="System.String"/>
        /// </param>
        public void SetTags(string[] tags)
        {
            List<Tag> list = new List<Tag>();
            foreach ( string name in tags )
                list.Add(new Tag(name, Session));

            SetTags(list.ToArray());
        }

        /// <summary>
        /// Sets the tags applied by the authenticated user to this artist to
        /// only those tags. Removing and adding tags as neccessary.
        /// </summary>
        /// <param name="tags">
        /// A <see cref="TagCollection"/>
        /// </param>
        public void SetTags(TagCollection tags)
        {
            SetTags(tags.ToArray());
        }

        /// <summary>
        /// Clears the tags that the authenticated user has set to this artist.
        /// </summary>
        public void ClearTags()
        {
            foreach ( Tag tag in GetTags() )
                RemoveTags(tag);
        }

        #endregion

        #endregion // Members

        #region Constructor

        public Artist(string name, Session session)
            : base(session)
        {
            Name = name;
        }

        #endregion // Constructor

        #region Methods

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

        /// <summary>
        /// Returns the similar artists to this artist ordered by similarity from the
        /// most similar to the least similar.
        /// </summary>
        /// <param name="limit">
        /// A <see cref="System.Int32"/>
        /// </param>
        /// <returns>
        /// A <see cref="Artist"/>
        /// </returns>
        public Artist[] GetSimilar(int limit, bool autocorrect = false, string mbid = null)
        {
            RequestParameters p = getParams();
            if ( limit > -1 )
                p["limit"] = limit.ToString(CultureInfo.InvariantCulture);
            if ( !string.IsNullOrWhiteSpace(mbid) )
                p["mbid"] = mbid;
            p["autocorrect"] = autocorrect ? "0" : "1";

            var doc = request("artist.getSimilar", p);
            var list = extract<ArtistArray>(doc, "similarartists");

            return list.Artists;
        }

        public Artist GetInfo()
        {
            RequestParameters p = getParams();
            p["lang"] = getLanguage();

            var jDoc = request("artist.getInfo", p);
            var ar = extract<Artist>(jDoc, "artist");
            ar.appendSession(Session);
            return ar;
        }

        public ArtistCurrection[] GetCorrections()
        {
            var jDoc = request("artist.getCorrection");
            var cr = extract<ArtistCurrectionArray>(jDoc, "corrections");
            return cr.Currections;
        }

        public void CorrectArtist()
        {
            var correction = GetCorrections();
            if ( correction != null )
                Name = correction[0].Artist.Name;
        }

        public Event[] GetEvents()
        {
            var jDoc = request("artist.getEvents");
            var events = extract<EventArray>(jDoc, "events");
            return events.Events;
        }

        public Event[] GetPastEvents()
        {           
            var jDoc = request("artist.getPastEvents");
            var events = extract<EventArray>(jDoc, "events");
            return events.Events;
        }

        public Rss GetPodcast()
        {
            var jDoc = request("artist.getPodcast");
            var events = extract<Rss>(jDoc, "rss");
            return events;
        }

        public Shout[] GetShouts()
        {
            var jDoc = request("artist.getShouts");
            var shouts = extract<ShoutArray>(jDoc, "shouts");
            return shouts.Shouts;
        }

        public Album[] GetTopAlbums(int page = 1, int limit = 50)
        {
            var p = getParams();
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);
            var jDoc = request("artist.getTopAlbums");
            var albums = extract<AlbumArray>(jDoc, "topalbums");
            return albums.Albums;
        }

        public User[] GetTopFans()
        {
            var jDoc = request("artist.getTopFans");
            var users = extract<UserArray>(jDoc, "topfans");
            return users.Users;
        }

        public Track[] GetTopTracks(int page = 1, int limit = 50)
        {
            var p = getParams();
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("artist.getTopTracks", p);
            var res = extract<TrackArray>(req, "toptracks");

            return res.Tracks;
        }

        public ArtistSearchResults Search(string term, int page = 1, int limit = 50)
        {
            var p = new RequestParameters();
            p["artist"] = term;
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);
            var jDoc = request("artist.search", p);
            var results = extract<ArtistSearchResults>(jDoc, "results");
            return results;
        }

        public void Shout(string message)
        {
            requireAuthentication();

            var p = getParams();
            p["message"] = message;          
            request("artist.shout");
        }

        #endregion

        #region Utilities

        internal override RequestParameters getParams()
        {
            var p = new RequestParameters();
            p["artist"] = this.Name;

            return p;
        }

        #endregion

        #region IEquatable<Artist> Members

        public bool Equals(Artist artist)
        {
            return ( artist.Name == this.Name );
        }

        #endregion

        public static implicit operator Artist(string artist)
        {
            return new Artist(artist, null);
        }
    }

    public class ArtistArray
    {
        [JsonConverter(typeof(JsonToArrayConverter<Artist>))]
        [JsonProperty(PropertyName = "artist")]
        public Artist[] Artists { get; set; }
    }
}
