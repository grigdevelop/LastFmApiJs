using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class Wiki
    {
        [JsonProperty("published")]
        public DateTime Published { get; private set; }

        [JsonProperty("summary")]
        public string Summary { get; private set; }

        [JsonProperty("content")]
        public string Content { get; private set; }
    }
}
