using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class Rss
    {
        [JsonProperty("channel")]
        public Channel Channel { get; private set; }
    }
}
