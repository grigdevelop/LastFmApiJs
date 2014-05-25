using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LastFmApiJsNet.Services;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Api
{
    public class Image
    {
        [JsonProperty(PropertyName = "#text")]
        public string Url { get; set; }

        public string Size { get; set; }
    }

    //[JsonConverter(typeof(JsonToArrayConverter<Image>))]
    public class Images : List<Image>
    {
        public string GetImageUrl(ImageSize imageSize)
        {
            string imageUrl = string.Empty;
            var image = this.ElementAtOrDefault((int)imageSize);
            if ( image != null )
                imageUrl = image.Url;
            return imageUrl;
        }
    }
}
