using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastFmApiJsNet.Services
{
    /// <summary>
    /// A collection of recipients.
    /// </summary>
    public class Recipients : List<string>
    {
        public Recipients()
            : base()
        {
        }

        /// <summary>
        /// Add a Last.fm username.
        /// </summary>
        /// <param name="username">
        /// A <see cref="System.String"/>
        /// </param>
        public new void Add(string username)
        {
            base.Add(username);
        }

        /// <summary>
        /// Add a <see cref="User"/>.
        /// </summary>
        /// <param name="user">
        /// A <see cref="User"/>
        /// </param>
        public void Add(User user)
        {
            base.Add(user.Name);
        }

        /// <summary>
        /// Add an email.
        /// </summary>
        /// <param name="email">
        /// A <see cref="System.String"/>
        /// </param>
        public void AddEmail(string email)
        {
            base.Add(email);
        }
    }
}
