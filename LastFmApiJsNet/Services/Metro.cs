using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LastFmApiJsNet.Api;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class Metro
    {
        /// <summary>
        /// The metro's name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; private set; }

        /// <summary>
        /// A country name, as defined by the ISO 3166-1 country names standard
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; private set; }
    }

    public class MetroArray
    {       
        [JsonProperty("metro")]
        [JsonConverter(typeof(JsonToArrayConverter<Metro>))]
        public Metro[] Metros { get; private set; }
    }
}
