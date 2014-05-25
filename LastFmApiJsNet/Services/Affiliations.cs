using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class Affiliations
    {
        [JsonProperty("physicals")]
        public AffiliationArray Physicals { get; private set; }

        [JsonProperty("downloads")]
        public AffiliationArray Downloads { get; private set; }
    }
}
