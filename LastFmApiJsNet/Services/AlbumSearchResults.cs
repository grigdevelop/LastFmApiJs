﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class AlbumSearchResults : SearchResults<Album>
    {
        [JsonProperty("albummatches")]
        public AlbumArray AlbumMatches { get; private set; }
    }
}
