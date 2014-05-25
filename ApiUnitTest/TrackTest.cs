using System;
using System.Linq;
using LastFmApiJsNet.Api;
using LastFmApiJsNet.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiUnitTest
{
    [TestClass]
    public class TrackTest
    {
        [TestMethod]
        public void GetTags()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var track = new Track("Muse", "Showbiz", session);
            var tags = track.GetTags("gArLiEgKoSr");
           
        }

        [TestMethod]
        public void GetTopTags()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var track = new Track("Muse", "Showbiz", session);
            var tags = track.GetTopTags();
            Assert.IsTrue(tags.Any());
        }

        [TestMethod]
        public void GetBuylinks()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var track = new Track("Muse", "Showbiz", session);
            var butlinks = track.GetBuylinks("United Kingdom");
            Assert.IsNotNull(butlinks);
        }

        [TestMethod]
        public void GetCurrection()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var track = new Track("Guns N' Roses", "Mrbrownstone", session);
            var c = track.GetCurrection();
            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void GetFingerprintMetadata()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var track = new Track("Guns N' Roses", "Mrbrownstone", session);
            var ss = track.GetFingerprintMetadata("1234");
            Assert.IsNotNull(ss);
        }

        [TestMethod]
        public void GetInfo()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var track = new Track("Muse", "Showbiz", session);
            var info = track.GetInfo("gArLiEgKoSr");
            Assert.IsNotNull(info);
        }

        [TestMethod]
        public void GetShouts()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var track = new Track("Muse", "Showbiz", session);
            var shouts = track.GetShouts();
            Assert.IsTrue(shouts.Any());
        }

        [TestMethod]
        public void GetSimilar()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var track = new Track("Muse", "Showbiz", session);
            var similars = track.GetSimilar();
            Assert.IsTrue(similars.Any());
        }

        [TestMethod]
        public void GetTopFans()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var track = new Track("Muse", "Showbiz", session);
            var fans = track.GetTopFans();
            Assert.IsTrue(fans.Any());
        }

        [TestMethod]
        public void Scrobble()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            session.Authenticate("gArLiEgKoSr", "poperKeepsAlive89");
            var track = new Track("Muse", "Showbiz", session);  
            // track.Scrobble(DateTime.UtcNow);
        }

        [TestMethod]
        public void Search()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var track = new Track("Muse", "Showbiz", session);
            var tracks = track.Search();
            Assert.IsNotNull(tracks);
        }


    }
}
