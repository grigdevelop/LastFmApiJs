using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class Channel
    {
        [JsonProperty("title")]
        public string Title { get; private set; }

        [JsonProperty("link")]
        public string Link { get; private set; }

        [JsonProperty("description")]
        public string Description { get; private set; }
    }
}
