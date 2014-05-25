using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using LastFmApiJsNet.Api;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LastFmApiJsNet.Services
{
    public abstract class Base
    {
        protected Session Session { get; set; }

        private JsonSerializer _jsonSerializer;
       
        public Base(Session session)
        {
            Session = session;

            _jsonSerializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            };
        }

        /// <summary>
        /// Add Session if you want user class methods
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public bool appendSession(Session session)
        {
            this.Session = session;
            return true;
        }

        internal abstract RequestParameters getParams();

        internal JObject request(string methodName, RequestParameters parameters)
        {
            return ( new Request(methodName, Session, parameters) ).Execute();
        }

        internal JObject request(string methodName)
        {
            return ( new Request(methodName, Session, getParams()) ).Execute();
        }

        //internal string extract(JObject node, string name, int index)
        //{
        //    return node.Property(name).Value.ToObject<JArray>()[index].Value<string>();
        //}

        //internal string extract(JObject node, string name)
        //{
        //    return node.Property(name).Value.ToString();
        //}

        internal T extract<T>(JObject jnode, string jName)
        {
            try
            {
                return jnode.Property(jName).Value.ToObject<T>(_jsonSerializer);
            }
            catch
            {

                return new JObject().ToObject<T>();
            }

        }

        internal void requireAuthentication()
        {
            if ( !this.Session.Authenticated )
                throw new AuthenticationRequiredException();
        }

        internal T[] sublist<T>(T[] original, int length)
        {
            List<T> list = new List<T>();

            for ( int i = 0; i < length; i++ )
                list.Add(original[i]);

            return list.ToArray();
        }

        internal string urlSafe(string text)
        {
            return HttpUtility.UrlEncode(HttpUtility.UrlEncode(text));
        }

        internal string getPeriod(Period period)
        {
            string[] values = new string[] { "overall", "3month", "6month", "12month" };

            return values[(int)period];
        }

        internal string getSiteDomain(SiteLanguage language)
        {
            Dictionary<SiteLanguage, string> domains = new Dictionary<SiteLanguage, string>();

            domains.Add(SiteLanguage.English, "www.last.fm");
            domains.Add(SiteLanguage.German, "www.lastfm.de");
            domains.Add(SiteLanguage.Spanish, "www.lastfm.es");
            domains.Add(SiteLanguage.French, "www.lastfm.fr");
            domains.Add(SiteLanguage.Italian, "www.lastfm.it");
            domains.Add(SiteLanguage.Polish, "www.lastfm.pl");
            domains.Add(SiteLanguage.Portuguese, "www.lastfm.com.br");
            domains.Add(SiteLanguage.Swedish, "www.lastfm.se");
            domains.Add(SiteLanguage.Turkish, "www.lastfm.com.tr");
            domains.Add(SiteLanguage.Russian, "www.lastfm.ru");
            domains.Add(SiteLanguage.Japanese, "www.lastfm.jp");
            domains.Add(SiteLanguage.Chinese, "cn.last.fm");

            return domains[language];
        }

        internal string getLanguage()
        {
            return getLanguage(Session.SiteLanguage);
        }

        internal string getLanguage(SiteLanguage siteLanguage)
        {           
            string lang = "en";
            switch ( siteLanguage )
            {
                case SiteLanguage.Chinese:
                    lang = "zh";
                    break;
                case SiteLanguage.English:
                    lang = "en";
                    break;
                case SiteLanguage.French:
                    lang = "fr";
                    break;
                case SiteLanguage.German:
                    lang = "de";
                    break;
                case SiteLanguage.Italian:
                    lang = "it";
                    break;
                case SiteLanguage.Japanese:
                    lang = "ja";
                    break;
                case SiteLanguage.Polish:
                    lang = "pl";
                    break;
                case SiteLanguage.Portuguese:
                    lang = "pt";
                    break;
                case SiteLanguage.Russian:
                    lang = "ru";
                    break;
                case SiteLanguage.Spanish:
                    lang = "es";
                    break;
                case SiteLanguage.Swedish:
                    lang = "sv";
                    break;
                case SiteLanguage.Turkish:
                    lang = "tr";
                    break;
            }
            return lang;
        }
    }
}
