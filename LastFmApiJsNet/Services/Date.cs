using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class Date
    {
        [JsonProperty("#text")]
        public DateTime TheDate { get; set; }

        [JsonProperty("urs")]
        public long UTS { get; set; }
    }
}
