﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LastFmApiJsNet.Api;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class Track : Base, IEquatable<Track>, ITaggable, IShareable
    {
        #region Setters

        [JsonProperty("toptags")]
        private TopTagArray _topTags { get; set; }

        #endregion

        #region Members

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("duration")]
        public int? Duration { get; private set; }

        [JsonProperty("playcount")]
        public int? Playcount { get; private set; }

        [JsonProperty("listeners")]
        public int? Listeners { get; private set; }

        [JsonProperty("mbid")]
        public string Mbid { get; private set; }

        [JsonProperty("url")]
        public string Url { get; private set; }

        [JsonProperty("artist")]
        public Artist Artist { get; protected set; }

        [JsonProperty("streamable")]
        public Streamable Streamable { get; private set; }

        [JsonProperty("album")]
        public Album Album { get; private set; }

        public TopTag[] TopTags
        {
            get { return _topTags == null ? null : _topTags.TopTags; }
        }

        [JsonProperty("wiki")]
        public Wiki Wiki { get; private set; }

        #endregion // Members

        #region Constructor

        [JsonConstructor]
        public Track(string artistName, string title, Session session)
            : base(session)
        {
            this.Artist = new Artist(artistName, session);
            this.Name = title;
        }

        public Track(Artist artist, string title, Session session)
            : base(session)
        {
            this.Artist = artist;
            this.Name = title;
        }

        #endregion // Constructor

        #region Methods

        /// <summary>
        /// Get a list of Buy Links for a particular Track. 
        /// It is required that you supply either the artist and track params or the mbid param.
        /// </summary>
        /// <param name="country">A country name or two character country code, as defined by the ISO 3166-1 country names standard.</param>
        /// <returns></returns>
        public Affiliations GetBuylinks(string country)
        {
            var p = getParams();
            p["country"] = country;

            var req = request("track.getBuylinks", p);
            var res = extract<Affiliations>(req, "affiliations");

            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TrackCurrection[] GetCurrection()
        {
            var req = request("track.getCorrection");
            var res = extract<TrackCurrectionArray>(req, "corrections");

            return res.Currections;
        }

        /// <summary>
        /// Retrieve track metadata associated with a fingerprint id generated by the Last.fm Fingerprinter. Returns track elements,
        ///  along with a 'rank' value between 0 and 1 reflecting the confidence for each match. 
        /// </summary>
        /// <param name="fingerprintid">The fingerprint id to look up</param>
        /// <returns></returns>
        public Track[] GetFingerprintMetadata(string fingerprintid)
        {
            var p = new RequestParameters();
            p["fingerprintid"] = fingerprintid;

            var req = request("track.getFingerprintMetadata", p);
            var res = extract<TrackArray>(req, "tracks");

            return res.Tracks;
        }

        /// <summary>
        /// Get the metadata for a track on Last.fm using the artist/track name or a musicbrainz id.
        /// </summary>
        /// <param name="username">The username for the context of the request. If supplied, the user's 
        /// playcount for this track and whether they have loved the track is included in the response.</param>
        /// <returns></returns>
        public Track GetInfo(string username = null)
        {
            var p = getParams();
            if (!string.IsNullOrWhiteSpace(username))
                p["username"] = username;

            var req = request("track.getInfo", p);
            var res = extract<Track>(req, "track");

            return res;
        }

        /// <summary>
        /// Get shouts for this track. Also available as an rss feed.
        /// </summary>
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 50.</param>
        /// <param name="autocorrect">Transform misspelled artist and track
        ///  names into correct artist and track names, returning the correct version 
        /// instead. The corrected artist and track name will be returned in the response.</param>
        /// <returns></returns>
        public Shout[] GetShouts(int page = 1, int limit = 50, int autocorrect = 1)
        {
            var p = getParams();
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);
            p["autocorrect"] = autocorrect.ToString(CultureInfo.InvariantCulture);

            var req = request("track.getShouts", p);
            var res = extract<ShoutArray>(req, "shouts");

            return res.Shouts;
        }

        /// <summary>
        /// Get the similar tracks for this track on Last.fm, based on listening data.
        /// </summary>
        /// <param name="limit">Maximum number of similar tracks to return</param>
        /// <param name="autocorrect">Transform misspelled artist and track names into correct artist 
        /// and track names, returning the correct version instead. The corrected artist and track 
        /// name will be returned in the response.</param>
        /// <returns></returns>
        public Track[] GetSimilar(int limit = 50, int autocorrect = 1)
        {
            var p = getParams();
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);
            p["autocorrect"] = autocorrect.ToString(CultureInfo.InvariantCulture);

            var req = request("track.getSimilar", p);
            var res = extract<TrackArray>(req, "similartracks");

            return res.Tracks;
        }

        /// <summary>
        /// Get the top fans for this track on Last.fm, based on listening data.
        ///  Supply either track & artist name or musicbrainz id.
        /// </summary>
        /// <param name="autocorrect"> Transform misspelled artist and track names into correct artist and track names,
        ///  returning the correct version instead. 
        /// The corrected artist and track name will be returned in the response.</param>
        /// <returns></returns>
        public User[] GetTopFans(int autocorrect = 1)
        {
            var p = getParams();
            p["autocorrect"] = autocorrect.ToString(CultureInfo.InvariantCulture);

            var req = request("track.getTopFans", p);
            var res = extract<UserArray>(req, "topfans");

            return res.Users;
        }

        /// <summary>
        /// Love a track for a user profile.
        /// </summary>
        public void Love()
        {
            requireAuthentication();
            request("track.love");
        }

        /// <summary>
        /// Used to add a track-play to a user's profile. Scrobble a track, 
        /// or a batch of tracks. Tracks are passed to the service using array
        ///  notation for each of the below params, up to a maximum of 50 scrobbles
        ///  per batch [0<=i<=49]. If you are only sending a single scrobble 
        /// the array notation may be ommited. Note: Extra care should be taken 
        /// while calculating the signature when using array notation as the
        ///  parameter names MUST be sorted according to the ASCII table (i.e.,
        ///  artist[10] comes before artist[1]). It is important to not use the 
        /// corrections returned by the now playing service as input for the scrobble
        ///  request, unless they have been explicitly approved by the user.
        ///  Parameter names are case sensitive.
        /// </summary>
        /// <param name="timestamp">The time the track started playing,
        ///  in UNIX timestamp format (integer number of seconds since
        ///  00:00:00, January 1st 1970 UTC). This must be in the UTC time zone.</param>
        public void Scrobble(DateTime timestamp)
        {
            requireAuthentication();
            var p = getParams();
            p["timestamp"] = Utilities.DateTimeToUTCTimestamp(timestamp).ToString(CultureInfo.InvariantCulture);

            request("track.scrobble", p);
        }

        /// <summary>
        /// Search for a track by track name. Returns track matches sorted by relevance.
        /// </summary>        
        /// <param name="page">The page number to fetch. Defaults to first page.</param>
        /// <param name="limit">The number of results to fetch per page. Defaults to 30.</param>
        /// <returns></returns>
        public TrackSearchResults Search(int page = 1, int limit = 30)
        {
            var p = getParams();
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("track.search", p);
            var res = extract<TrackSearchResults>(req, "results");

            return res;
        }

        /// <summary>
        /// Share this track with others.
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

            request("track.Share", p);
        }

        /// <summary>
        /// Share this track with others.
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

            request("track.Share", p);
        }

        /// <summary>
        /// UnBan a track for a user profile.
        /// </summary>
        public void UnBan()
        {
            requireAuthentication();
            request("track.unban");
        }

        /// <summary>
        /// UnLove a track for a user profile.
        /// </summary>
        public void UnLove()
        {
            requireAuthentication();
            request("track.unlove");
        }

        /// <summary>
        /// Used to notify Last.fm that a user has started listening to a track. Parameter names are case sensitive.
        /// </summary>
        /// <param name="album">The album name.</param>
        /// <param name="trackNumber">The track number of the track on the album.</param>
        /// <param name="context">Sub-client version (not public, only enabled for certain API keys)</param>
        /// <param name="duration">The length of the track in seconds.</param>
        /// <param name="albumArtist">The album artist - if this differs from the track artist.</param>
        public void UpdateNowPlaying(string album = null, int? trackNumber = null,
            string context = null, int? duration = null, string albumArtist = null)
        {
            requireAuthentication();
            var p = getParams();
            if (!string.IsNullOrWhiteSpace(album))
                p["album"] = album;
            if (trackNumber.HasValue)
                p["trackNumber"] = trackNumber.Value.ToString(CultureInfo.InvariantCulture);
            if (!string.IsNullOrWhiteSpace(context))
                p["context"] = context;
            if (duration.HasValue)
                p["duration"] = duration.Value.ToString(CultureInfo.InvariantCulture);
            if (!string.IsNullOrWhiteSpace(albumArtist))
                p["albumArtist"] = albumArtist;

            request("track.updateNowPlaying", p);           
        }

        #endregion

        #region Utilities

        internal override RequestParameters getParams()
        {
            var p = new RequestParameters();
            p["artist"] = Artist.Name;
            p["track"] = Name;

            return p;
        }

        #endregion

        #region IEquatable<Track> Members

        public bool Equals(Track other)
        {
            return ( other.Name == this.Name && other.Artist.Name == this.Artist.Name );
        }

        #endregion

        #region ITaggable Members

        /// <summary>
        /// Tag an album using a list of user supplied tags.
        /// </summary>
        /// <param name="tags"> A comma delimited list of user supplied tags to apply to this track. 
        /// Accepts a maximum of 10 tags.</param>
        public void AddTags(params Tag[] tags)
        {
            //This method requires authentication
            requireAuthentication();

            foreach ( Tag tag in tags )
            {
                RequestParameters p = getParams();
                p["tags"] = tag.Name;

                request("track.addTags", p);
            }
        }

        /// <summary>
        /// Tag an album using a list of user supplied tags.
        /// </summary>
        /// <param name="tags"> A comma delimited list of user supplied tags to apply to this track.
        ///  Accepts a maximum of 10 tags.</param>
        public void AddTags(params string[] tags)
        {
            foreach ( string tag in tags )
                AddTags(new Tag(tag, Session));
        }

        /// <summary>
        /// Tag an album using a list of user supplied tags.
        /// </summary>
        /// <param name="tags"> A comma delimited list of user supplied tags to apply to this track.
        ///  Accepts a maximum of 10 tags.</param>
        public void AddTags(TagCollection tags)
        {
            foreach ( Tag tag in tags )
                AddTags(tag);
        }

        /// <summary>
        /// Get the tags applied by an individual user to a track on Last.fm. 
        /// To retrieve the list of top tags applied to a track by all users use track.getTopTags.
        /// </summary>
        /// <param name="username">If called in non-authenticated mode you must specify the user to look up</param>
        /// <returns></returns>
        public Tag[] GetTags(string username)
        {
            var p = getParams();
            if ( !string.IsNullOrWhiteSpace(username) )
                p["user"] = username;

            var req = request("track.getTags", p);
            var res = extract<TagArray>(req, "tags");

            return res.Tags;
        }

        /// <summary>
        /// Get the tags applied by an individual user to a track on Last.fm. 
        /// To retrieve the list of top tags applied to a track by all users use track.getTopTags.
        /// </summary>
        /// <returns></returns>
        public Tag[] GetTags()
        {
            return GetTags(null);
        }


        /// <summary>
        /// Get the top tags for this track on Last.fm, ordered by tag count. Supply either track & artist name or mbid.
        /// </summary>
        /// <returns></returns>
        public TopTag[] GetTopTags()
        {
            var req = request("track.getTopTags");
            var res = extract<TopTagArray>(req, "toptags");

            return res.TopTags;
        }

        /// <summary>
        /// Get the top tags for this track on Last.fm, ordered by tag count. Supply either track & artist name or mbid.
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public TopTag[] GetTopTags(int limit)
        {
            var toptags = GetTopTags().Take(limit);
            return toptags == null ? null : toptags.Take(limit).ToArray();
        }

        /// <summary>
        /// Remove a user's tags from a track.
        /// </summary>
        /// <param name="tags"></param>
        public void RemoveTags(params string[] tags)
        {
            //This method requires authentication
            requireAuthentication();

            foreach ( string tag in tags )
                RemoveTags(new Tag(tag, Session));
        }

        /// <summary>
        /// Remove a user's tags from a track.
        /// </summary>
        /// <param name="tags"></param>
        public void RemoveTags(params Tag[] tags)
        {
            //This method requires authentication
            requireAuthentication();

            foreach ( Tag tag in tags )
            {
                RequestParameters p = getParams();
                p["tag"] = tag.Name;

                request("track.removeTag", p);
            }
        }

        /// <summary>
        /// Remove a user's tags from a track.
        /// </summary>
        /// <param name="tags"></param>
        public void RemoveTags(TagCollection tags)
        {
            foreach ( Tag tag in tags )
                RemoveTags(tag);
        }

        /// <summary>
        /// Tag an album using a list of user supplied tags.
        /// </summary>
        /// <param name="tags"></param>
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
        /// Tag an album using a list of user supplied tags.
        /// </summary>
        /// <param name="tags"></param>
        public void SetTags(string[] tags)
        {
            List<Tag> list = new List<Tag>();
            foreach ( string name in tags )
                list.Add(new Tag(name, Session));

            SetTags(list.ToArray());
        }

        /// <summary>
        /// Tag an album using a list of user supplied tags.
        /// </summary>
        /// <param name="tags"></param>
        public void SetTags(TagCollection tags)
        {
            SetTags(tags.ToArray());
        }

        public void ClearTags()
        {
            foreach ( Tag tag in GetTags() )
                RemoveTags(tag);
        }

        #endregion
    }

    public class TrackArray
    {
        [JsonConverter(typeof(JsonToArrayConverter<Track>))]
        [JsonProperty("track")]
        public Track[] Tracks { get; set; }
    }

}
