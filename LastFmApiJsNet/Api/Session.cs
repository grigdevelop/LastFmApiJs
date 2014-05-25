using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LastFmApiJsNet.Services;
using Newtonsoft.Json.Linq;

namespace LastFmApiJsNet.Api
{
    [Serializable]
    public class Session : IEquatable<Session>
    {
        #region Members

        /// <summary>
        /// The Last.Fm API key
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// The Last.Fm API secret
        /// </summary>
        public string ApiSecret { get; set; }

        /// <summary>
        /// The Last.Fm account session key
        /// </summary>
        public string SessionKey { get; set; }

        public SiteLanguage SiteLanguage { get; set; }

        /// <summary>
        /// Returns true if the session is authenticated.
        /// </summary>
        public bool Authenticated
        {
            get { return SessionKey != null; }
        }

        private string token { get; set; }

        #endregion // Members

        #region Constructors

        public Session(string apiKey, string apiSecret, SiteLanguage siteLanguage = SiteLanguage.English)
        {
            ApiKey = apiKey;
            ApiSecret = apiSecret;
            this.SiteLanguage = siteLanguage;
        }

        #endregion // Constructors

        #region Methods

        public void Authenticate(string username, string password)
        {
            var p = new RequestParameters();

            p["password"] = password;
            p["username"] = username;

            Request request = new Request("auth.getMobileSession", this, p, true);
            request.signIt();
            string rrr = request.Parameters.ToString();
            var doc = request.Execute();

            SessionKey = doc.Property("session").Value.ToObject<JObject>().Property("key").Value.ToString();
        }

        /// <summary>
        /// Complete the web authentication.
        /// </summary>
        public void AuthenticateViaWeb()
        {
            RequestParameters p = new RequestParameters();
            p["token"] = token;

            Request r = new Request("auth.getSession", this, p);
            r.signIt();

            var doc = r.Execute();

            SessionKey = doc.Property("session").Value.ToObject<JObject>().Property("key").Value.ToString();
        }

        public string GetWebAuthenticationURL()
        {
            token = getAuthenticationToken();

            return "http://www.last.fm/api/auth/?api_key=" + ApiKey + "&token=" + token;
        }

        #endregion // Methods

        #region Utilities

        private string getAuthenticationToken()
        {
            JObject doc = ( new Request("auth.getToken", this, new RequestParameters()) ).Execute();

            return doc.Property("token").Value.ToString();
        }

        #endregion // Utilities

        #region IEquatable<Session> Members

        /// <summary>
        /// Check to see if this object equals another.
        /// </summary>
        /// <param name="session">Session</param>
        /// <returns>boolean</returns>
        public bool Equals(Session session)
        {
            return ( session.ApiKey == ApiKey &&
                    session.ApiSecret == ApiSecret &&
                    session.SessionKey == SessionKey );
        }

        #endregion
    }
}
