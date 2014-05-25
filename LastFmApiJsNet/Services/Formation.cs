using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LastFmApiJsNet.Api;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class Formation
    {
        [JsonProperty(PropertyName = "yearfrom")]
        public int? YearFrom { get; private set; }

        [JsonProperty(PropertyName = "yearto")]
        public int? YearTo { get; private set; }
    }

    public class FormationArray
    {
        [JsonConverter(typeof(JsonToArrayConverter<Formation>))]
        [JsonProperty(PropertyName = "formation")]
        public Formation[] Formations { get; private set; }
    }
}
