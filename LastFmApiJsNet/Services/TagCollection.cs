using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LastFmApiJsNet.Api;

namespace LastFmApiJsNet.Services
{
    public class TagCollection : List<Tag>
    {
        private Session session { get; set; }

        public TagCollection(Session session)
            : base()
        {
            this.session = session;
        }
    }
}
