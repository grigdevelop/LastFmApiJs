using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class SearchResults<T>
    {
        [JsonProperty("opensearch:totalResults")]
        public int TotalResults { get; private set; }

        [JsonProperty("opensearch:startIndex")]
        public int StartIndex { get; private set; }

        [JsonProperty("opensearch:itemsPerPage")]
        public int ItemsPerPage { get; private set; }

        [JsonProperty("opensearch:Query")]
        public SearchQuery SearchQuery { get; private set; }
    }

    public class SearchQuery
    {
        [JsonProperty("#text")]
        public string Text { get; private set; }

        [JsonProperty("role")]
        public string Role { get; private set; }

        [JsonProperty("searchTerms")]
        public string SearchTerms { get; private set; }

        [JsonProperty("startPage")]
        public int StartPage { get; private set; }
    }
}
