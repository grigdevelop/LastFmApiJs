using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class TagSearchResults : SearchResults<Tag>
    {
        [JsonProperty("tagmatches")]
        private TagArray TagMatchesSetter { get; set; }

        [JsonIgnore]
        public Tag[] Tags
        {
            get { return TagMatchesSetter == null ? null : TagMatchesSetter.Tags; }
        }
    }
}
