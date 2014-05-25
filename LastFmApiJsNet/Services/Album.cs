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
    public class Album : Base, IHasImage, IEquatable<Album>, IHasURL, IShareable
    {
        #region Setters

        [JsonProperty("toptags")]
        private TagArray TopTagsSetter { get; set; }

        #endregion

        #region Members

        /// <summary>
        /// The album title/name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; private set; }

        /// <summary>
        /// The album's artist.
        /// </summary>
        [JsonProperty("artist")]
        public Artist Artist { get; private set; }

        [JsonProperty("id")]
        public int Id { get; private set; }

        [JsonProperty("mbid")]
        public string Mbid { get; private set; }

        [JsonProperty("releasedate")]
        public DateTime ReleaseDate { get; private set; }

        [JsonProperty("listeners")]
        public int Listeners { get; private set; }

        [JsonProperty("playcount")]
        public int Playcount { get; private set; }

        [JsonProperty("tracks")]
        public TrackArray Tracks { get; private set; }

        public Tag[] TopTags
        {
            get { return TopTagsSetter == null ? null : TopTagsSetter.Tags; }
        }

        [JsonProperty("wiki")]
        public Wiki Wiki { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Createa an album object.
        /// </summary>
        /// <param name="artistName">
        /// A <see cref="System.String"/>
        /// </param>
        /// <param name="title">
        /// A <see cref="System.String"/>
        /// </param>
        /// <param name="session">
        /// A <see cref="Session"/>
        /// </param>
        public Album(string artistName, string title, Session session)
            : base(session)
        {
            Artist = new Artist(artistName, Session);
            Name = title;
        }

        /// <summary>
        /// Create an album.
        /// </summary>
        /// <param name="artist">
        /// A <see cref="Artist"/>
        /// </param>
        /// <param name="title">
        /// A <see cref="System.String"/>
        /// </param>
        /// <param name="session">
        /// A <see cref="Session"/>
        /// </param>
        [JsonConstructor]
        public Album(Artist artist, string title, Session session)
            : base(session)
        {
            Artist = artist;
            Name = title;
        }

        #endregion // Constructors

        #region Methods

        /// <summary>
        /// Add tags to this album.
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

                request("album.addTags", p);
            }
        }

        /// <summary>
        /// Add tags to this album.
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
        /// Add tags to this album.
        /// </summary>
        /// <param name="tags">
        /// A <see cref="TagCollection"/>
        /// </param>
        public void AddTags(TagCollection tags)
        {
            foreach ( Tag tag in tags )
                AddTags(tag);
        }

        public Affiliations GetBuyLinks(string country)
        {
            var p = getParams();
            p["country"] = country;

            var req = request("album.getBuylinks", p);
            var res = extract<Affiliations>(req, "affiliations");

            return res;
        }

        public Album GetInfo()
        {
            var p = getParams();
            p["lang"] = getLanguage();

            var req = request("album.getInfo", p);
            var res = extract<Album>(req, "album");

            return res;
        }

        public Shout[] GetShouts(int page = 1, int limit = 50)
        {
            var p = getParams();
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);
            p["page"] = page.ToString(CultureInfo.InvariantCulture);

            var req = request("album.getShouts", p);
            var res = extract<ShoutArray>(req, "shouts");

            return res.Shouts;
        }

        /// <summary>
        /// Returns the tags set by the authenticated user to this album.
        /// </summary>
        /// <returns>
        /// A <see cref="Tag"/>
        /// </returns>
        public Tag[] GetTags()
        {
            //This method requires authentication
            requireAuthentication();

            var req = request("album.getTags");
            var res = extract<TagArray>(req, "tags");

            return res.Tags;
        }

        public TopTag[] GetTopTags()
        {
            var req = request("album.getTopTags");
            var res = extract<TopTagArray>(req, "toptags");

            return res.TopTags;
        }

        /// <summary>
        /// Remove from your tags on this album.
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

                request("album.removeTag", p);
            }
        }

        /// <summary>
        /// Remove from your tags on this album.
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
        /// Remove from the authenticated user's tags on this album.
        /// </summary>
        /// <param name="tags">
        /// A <see cref="TagCollection"/>
        /// </param>
        public void RemoveTags(TagCollection tags)
        {
            foreach ( Tag tag in tags )
                RemoveTags(tag);
        }


        public AlbumSearchResults Search(string term, int page = 1, int limit = 50)
        {
            var p = new RequestParameters();
            p["album"] = term;
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);
            var jDoc = request("album.search", p);
            var results = extract<AlbumSearchResults>(jDoc, "results");
            return results;
        }

        #endregion // Methods

        #region Utilities

        internal override RequestParameters getParams()
        {
            var p = new RequestParameters();
            p["artist"] = Artist.Name;
            p["album"] = Name;

            return p;
        }

        #endregion

        #region IHasImage Members

        public Images Images { get; private set; }

        #endregion

        #region IEquatable<Album> Members

        public bool Equals(Album album)
        {
            return ( album.Name == this.Name && album.Artist.Name == this.Artist.Name );
        }

        #endregion

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

            return "http://" + domain + "/music/" + urlSafe(Artist.Name) + "/" + urlSafe(Name);
        }

        /// <value>
        /// The Last.fm page of this object.
        /// </value>
        public string URL
        { get { return GetURL(SiteLanguage.English); } }

        #endregion


        #region IShareable Members

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

            request("album.Share", p);
        }

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

            request("album.Share", p);
        }

        #endregion
    }

    public class AlbumArray
    {
        [JsonConverter(typeof(JsonToArrayConverter<Album>))]
        [JsonProperty("album")]
        public Album[] Albums { get; private set; }
    }
}
