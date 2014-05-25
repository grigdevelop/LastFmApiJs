using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LastFmApiJsNet.Api;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    /// <summary>
    /// Periods, expressed as date ranges which can be sent to the chart services.
    /// </summary>
    public class WeeklyRange
    {
        /// <summary>
        /// Beginning timestamp of the weekly range requested
        /// </summary>
        [JsonProperty("from")]
        [JsonConverter(typeof(TimespanToDateTimeConvert))]
        public DateTime Start { get; private set; }

        /// <summary>
        /// Ending timestamp of the weekly range requested 
        /// </summary>
        [JsonProperty("to")]
        [JsonConverter(typeof(TimespanToDateTimeConvert))]
        public DateTime End { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start">Beginning timestamp of the weekly range requested </param>
        /// <param name="end">Ending timestamp of the weekly range requested </param>
        public WeeklyRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
    }

    public class WeeklyRangeArray
    {        
        /// <summary>
        /// Get a list of available chart, expressed as date ranges which can be sent to the chart services.
        /// </summary>
        [JsonProperty("chart")]
        [JsonConverter(typeof(JsonToArrayConverter<WeeklyRange>))]
        public WeeklyRange[] WeeklyRanges { get; private set; }
    }
}
