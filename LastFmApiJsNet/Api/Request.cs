using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LastFmApiJsNet.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LastFmApiJsNet.Api
{
    internal class Request
    {
        #region Fields

        public string ROOT = "http://ws.audioscrobbler.com/2.0/";
        public string SROOT = "https://ws.audioscrobbler.com/2.0/";

        #endregion // Fields

        #region Members

        public string MethodName { get; private set; }
        public Session Session { get; private set; }
        public bool SecureConnection { get; set; }
        public RequestParameters Parameters { get; private set; }
        internal static DateTime? lastCallTime { get; set; }

        #endregion // Members

        #region Constructors

        public Request(string methodName, Session session, RequestParameters parameters,
            bool secureConnection = false)
        {
            this.MethodName = methodName;
            this.Session = session;
            this.Parameters = parameters;
            this.SecureConnection = secureConnection;

            this.Parameters["method"] = this.MethodName;
            this.Parameters["api_key"] = this.Session.ApiKey;
            if ( Session.Authenticated )
            {
                this.Parameters["sk"] = this.Session.SessionKey;
                signIt();
            }
            this.Parameters["format"] = "json";
        }

        #endregion // Constructors

        #region Methods

        internal void signIt()
        {
            // because auth.getSession requires a signature without session key. 
            this.Parameters["api_sig"] = this.getSignature();
        }

        public JObject Execute()
        {

            // Go on normally from here.
            byte[] data = Parameters.ToBytes();
            ServicePointManager.Expect100Continue = false;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(SecureConnection ? SROOT : ROOT);
            request.ContentLength = data.Length;
            request.UserAgent = Utilities.UserAgent;
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            request.Headers["Accept-Charset"] = "utf-8";

            if ( Lib.Proxy != null )
                request.Proxy = Lib.Proxy;

            Stream writeStream = request.GetRequestStream();
            writeStream.Write(data, 0, data.Length);
            writeStream.Close();

            HttpWebResponse webresponse;
            try
            {
                webresponse = (HttpWebResponse)request.GetResponse();
            }
            catch ( WebException e )
            {
                webresponse = (HttpWebResponse)e.Response;
            }

            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            // Pipe the stream to a higher level stream reader with the required encoding format. 
            StreamReader readStream = new StreamReader(webresponse.GetResponseStream(), encode);
            string str = readStream.ReadToEnd();
            readStream.Close();

            JObject responseJObject = JsonConvert.DeserializeObject<JObject>(str);
            checkForErrors(responseJObject);

            return responseJObject;
        }

        #endregion // Methods

        #region Utilities

        private string getSignature()
        {
            string str = "";
            foreach ( string key in this.Parameters.Keys )
            {
                if ( key == "format" ) continue;
                str += key + this.Parameters[key];
            }

            str += this.Session.ApiSecret;

            return Utilities.MD5(str);

        }

        private void checkForErrors(JObject obj)
        {
            var error = obj.Property("error");
            if ( error != null )
            {
                int errorCode = int.Parse(error.Value.ToString());
                string errorMessage = obj.Property("message").ToString();
                throw new ServiceException((ServiceExceptionType)errorCode, errorMessage);
            }
        }

        #endregion // Utilities
    }
}
