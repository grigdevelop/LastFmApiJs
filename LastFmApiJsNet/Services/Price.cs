using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class Price
    {
        [JsonProperty("currency")]
        public string Currency { get; private set; }

        [JsonProperty("amount")]
        public decimal? Amount { get; private set; }

        [JsonProperty("formatted")]
        public string Formatted { get; private set; }


    }
}
