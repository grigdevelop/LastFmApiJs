using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class GeoPoint
    {
        [JsonProperty(PropertyName = "geo:lat")]
        public double? Latitude { get; private set; }

        [JsonProperty(PropertyName = "geo:long")]
        public double? Longitude { get; private set; }
    }
}
