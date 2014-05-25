using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class Location
    {
        [JsonProperty(PropertyName = "geo:point")]
        public GeoPoint GeoPoint { get; private set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "street")]
        public string Street { get; set; }

        [JsonProperty(PropertyName = "postalcode")]
        public string PostalCode { get; set; }
    }
}
