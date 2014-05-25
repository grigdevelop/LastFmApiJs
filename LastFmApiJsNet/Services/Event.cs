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
    public class Event : Base, IHasImage, IShareable
    {
        #region Members

        /// <summary>
        /// The event ID.
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; private set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; private set; }

        [JsonConverter(typeof(JsonToArrayConverter<EventArtist>))]
        [JsonProperty(PropertyName = "artists")]
        public EventArtist[] Artists { get; private set; }

        [JsonProperty(PropertyName = "venue")]
        public Venue Venue { get; private set; }

        [JsonProperty(PropertyName = "startDate")]
        public DateTime StartDate { get; private set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; private set; }

        [JsonProperty("url")]
        public string Url { get; private set; }

        [JsonProperty("website")]
        public string Website { get; private set; }

        [JsonProperty("phonenumber")]
        public string PhoneNumber { get; private set; }

        [JsonProperty("attendance")]
        public int Attendance { get; private set; }

        [JsonProperty("reviews")]
        public int Reviews { get; private set; }

        [JsonProperty("tag")]
        public string Tag { get; private set; }

        [JsonProperty("tickets")]
        public string Tickets { get; private set; }

        [JsonProperty("cancelled")]
        public int Cancelled { get; private set; }

        #endregion // Members

        #region Constructor

        public Event(int id, Session session)
            : base(session)
        {
            ID = id;
        }

        #endregion

        #region Methods

        public void Attend(EventAttendance attendance)
        {
            requireAuthentication();

            RequestParameters p = getParams();
            int i = (int)attendance;
            p["status"] = i.ToString(CultureInfo.InvariantCulture);
            request("event.attend", p);
        }

        public User[] GetAttendees(int page = 1, int limit = 50)
        {
            var p = getParams();
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("event.getAttendees", p);
            var res = extract<UserArray>(req, "attendees");

            return res.Users;
        }

        public Event GetInfo()
        {
            var req = request("event.getInfo");
            var res = extract<Event>(req, "event");

            return res;
        }

        public Shout[] GetShouts(int page = 1, int limit = 50)
        {
            var p = getParams();
            p["page"] = page.ToString(CultureInfo.InvariantCulture);
            p["limit"] = limit.ToString(CultureInfo.InvariantCulture);

            var req = request("event.getShouts", p);
            var res = extract<ShoutArray>(req, "shouts");

            return res.Shouts;
        }

        public void Shout(string message)
        {
            requireAuthentication();

            var p = getParams();

            request("event.shout", p);
        }

        #endregion

        #region Utilities

        internal override RequestParameters getParams()
        {
            var p = new RequestParameters();
            p["event"] = ID.ToString(CultureInfo.InvariantCulture);

            return p;
        }

        #endregion

        #region IHasImage Members

        [JsonProperty(PropertyName = "image")]
        public Images Images { get; private set; }

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

            request("event.Share", p);
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

            request("event.Share", p);
        }

        #endregion
    }

    public class EventArray
    {
        [JsonConverter(typeof(JsonToArrayConverter<Event>))]
        [JsonProperty(PropertyName = "event")]
        public Event[] Events { get; set; }
    }
}
