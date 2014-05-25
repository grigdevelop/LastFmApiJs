using System;
using System.Net;

namespace LastFmApiJsNet.Api
{
    public static class Lib
    {
        /// <summary>
        /// A <see cref="IWebProxy"/>.
        /// </summary>
        /// <value>
        /// A web proxy to be used in making all the calls to Last.fm.
        /// </value>
        /// <remarks>
        /// To enable using a proxy server, set this value to a <see cref="IWebProxy"/>, like <see cref="WebProxy"/>.
        /// To disable using a proxy server, set it to null.
        /// 
        /// Default value is null.
        /// </remarks>
        public static IWebProxy Proxy { get; set; }

        /// <summary>
        /// Returns the version of this assembly.
        /// </summary>
        public static Version Version
        {
            get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version; }
        }
    }
}
