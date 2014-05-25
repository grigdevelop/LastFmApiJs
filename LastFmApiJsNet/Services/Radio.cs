using LastFmApiJsNet.Api;
using Newtonsoft.Json;

namespace LastFmApiJsNet.Services
{
    public class Radio : Base
    {
        #region Constructor

        [JsonConstructor]
        public Radio(Session session)
            : base(session)
        {

        }

        #endregion // Constructor

        #region Methods

        

        #endregion // Methods

        #region Utilities

        internal override RequestParameters getParams()
        {
            return new RequestParameters();
        }

        #endregion
      
    }
}
