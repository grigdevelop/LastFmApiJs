using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LastFmApiJsNet.Api
{
    public static class Utilities
    {
        internal static string UserAgent
        {
            get { return "LastFmApiJsNet/" + Lib.Version.ToString(); }
        }

        /// <summary>
        /// Returns the md5 hash of a string.
        /// </summary>
        /// <param name="text">
        /// A <see cref="System.String"/>
        /// </param>
        /// <returns>
        /// A <see cref="System.String"/>
        /// </returns>
        public static string MD5(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);

            MD5CryptoServiceProvider c = new MD5CryptoServiceProvider();
            buffer = c.ComputeHash(buffer);

            StringBuilder builder = new StringBuilder();
            foreach ( byte b in buffer )
                builder.Append(b.ToString("x2").ToLower());

            return builder.ToString();
        }

        public static DateTime TimestampToDateTime(long timestamp, DateTimeKind kind)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, kind).AddSeconds(timestamp).ToLocalTime();
        }

        public static long DateTimeToUTCTimestamp(DateTime dateTime)
        {
            DateTime baseDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            TimeSpan span = dateTime.ToUniversalTime() - baseDate;

            return (long)span.TotalSeconds;
        }
    }
}
