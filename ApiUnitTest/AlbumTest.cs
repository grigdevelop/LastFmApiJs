using System;
using System.Linq;
using LastFmApiJsNet.Api;
using LastFmApiJsNet.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiUnitTest
{
    [TestClass]
    public class AlbumTest
    {
  
        [TestMethod]
        public void GetBuyLinks()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var album = new Album("radiohead", "Pablo Honey", session);
            var buyLinks = album.GetBuyLinks("united kingdom");
            Assert.IsTrue(buyLinks.Downloads.Affiliations.Any());
            Assert.IsTrue(buyLinks.Physicals.Affiliations.Any());
        }

        [TestMethod]
        public void GetInfo()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var album = new Album("radiohead", "Pablo Honey", session);
            var info = album.GetInfo();
            Assert.IsNotNull(info.Artist);    
            Assert.IsTrue(info.Tracks.Tracks.Any());
            Assert.IsTrue(info.TopTags.Any());
        }

        [TestMethod]
        public void GetShouts()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var album = new Album("radiohead", "Pablo Honey", session);
            var shouts = album.GetShouts(1, 10);
            Assert.IsTrue(shouts.Length == 10);
        }

        [TestMethod]
        public void GetTags()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            session.Authenticate("gArLiEgKoSr", "poperKeepsAlive89");
            var album = new Album("radiohead", "Pablo Honey", session);
            var tags = album.GetTags();
            Assert.IsNotNull(tags);
        }

        [TestMethod]
        public void GetTopTags()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");            
            var album = new Album("radiohead", "Pablo Honey", session);
            var tags = album.GetTopTags();
            Assert.IsTrue(tags.Any());
        }

        [TestMethod]
        public void Search()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var album = new Album("radiohead", "Pablo Honey", session);
            var albums = album.Search("pa");
            Assert.IsTrue(albums.AlbumMatches.Albums.Any());
            Assert.IsNotNull(albums.SearchQuery);
            Assert.IsTrue("pa" == albums.SearchQuery.SearchTerms);
        }

    }
}
