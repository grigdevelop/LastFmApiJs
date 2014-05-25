using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class Streamable
    {
        [JsonProperty("fulltrack")]
        public int FullTrack { get; private set; }

        public static implicit operator Streamable(string streamable)
        {
            return new Streamable
            {
                FullTrack = int.Parse(streamable)
            };
        }
    }
}
