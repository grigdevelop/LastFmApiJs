using System;
using System.Linq;
using LastFmApiJsNet.Api;
using LastFmApiJsNet.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiUnitTest
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void GetArtistTracks()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var user = new User("gArLiEgKoSr", session);
            var tracks = user.GetArtistTracks("Muse");
            Assert.IsNotNull(tracks);
            Assert.IsTrue(tracks.Any());
            Assert.IsNotNull(tracks.FirstOrDefault().Artist);
        }
    }
}
