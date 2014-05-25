using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LastFmApiJsNet.Api;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class Affiliation
    {
        [JsonProperty("supplierName")]
        public string SupplierName { get; private set; }

        [JsonProperty("price")]
        public Price Price { get; private set; }

        [JsonProperty("buylink")]
        public string BuyLink { get; private set; }

        [JsonProperty("supplierIcon")]
        public string SupplierIcon { get; private set; }

        [JsonProperty("isSearch")]
        public int IsSearch { get; private set; }
    }

    public class AffiliationArray
    {
        [JsonConverter(typeof(JsonToArrayConverter<Affiliation>))]
        [JsonProperty("affiliation")]
        public Affiliation[] Affiliations { get; private set; }
    }
}
