using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LastFmApiJsNet.Api;

namespace LastFmApiJsNet.Services
{
    /// <summary>
    /// This exception is thrown whenever a method is called thar required an authenticated 
    /// <see cref="Session"/> object and the given was not.
    /// </summary>
    public class AuthenticationRequiredException : Exception
    {
        internal AuthenticationRequiredException()
            : base("This method requires an authenticated Session object.")
        {
        }
    }
}
