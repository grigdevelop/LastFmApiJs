using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LastFmApiJsNet.Api;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class Venue : IHasImage
    {
        #region Members

        [JsonProperty(PropertyName = "id")]
        public int ID { get; private set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; private set; }

        [JsonProperty(PropertyName = "website")]
        public string WebSite { get; private set; }

        [JsonProperty(PropertyName = "phonenumber")]
        public string PhoneNumber { get; private set; }       

        #endregion // Members

        #region IHasImage Members

        public Images Images { get; private set; }

        #endregion
    }
}
