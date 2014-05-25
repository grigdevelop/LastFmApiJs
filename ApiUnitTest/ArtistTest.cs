using System;
using System.Linq;
using LastFmApiJsNet.Api;
using LastFmApiJsNet.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiUnitTest
{
    [TestClass]
    public class ArtistTest
    {
        [TestMethod]
        public void GetPastEvents()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var artist = new Artist("Dream Theatre", session);
            var events = artist.GetPastEvents();
            Assert.IsTrue(events.Any());           
        }

        [TestMethod]
        public void GetPodcast()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var artist = new Artist("Dream Theater", session);
            var podcast = artist.GetPodcast();
            Assert.IsNotNull(podcast.Channel);
        }

        [TestMethod]
        public void GetEvents()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var artist = new Artist("Dream Theater", session);
            var events = artist.GetEvents();
            Assert.IsTrue(events.Any());
        }

        [TestMethod]
        public void GetShouts()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var artist = new Artist("Dream Theater", session);
            var shouts = artist.GetShouts();
            Assert.IsTrue(shouts.Any());
        }

        [TestMethod]
        public void GetTopAlbums()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var artist = new Artist("Dream Theater", session);
            var albums = artist.GetTopAlbums();
            Assert.IsTrue(albums.Any());
        }

        [TestMethod]
        public void GetTopFans()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var artist = new Artist("Dream Theater", session);
            var fans = artist.GetTopFans();
            Assert.IsTrue(fans.Any());
        }

        [TestMethod]
        public void GetTopTracks()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var artist = new Artist("Dream Theater", session);
            var tracks = artist.GetTopTracks();
            Assert.IsTrue(tracks.Any());
        }

        [TestMethod]
        public void Search()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var artist = new Artist("Dream Theater", session);
            var tracks = artist.Search("d");
            Assert.IsTrue(tracks.ArtistMatches.Artists.Any());
        }
    }
}
