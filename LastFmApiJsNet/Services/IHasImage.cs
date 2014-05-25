using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Api
{
    /// <summary>
    /// Objects that implement this has an image url available.
    /// </summary>
    public interface IHasImage
    {
        [JsonProperty(PropertyName = "image")]
        Images Images { get; }
    }
}
