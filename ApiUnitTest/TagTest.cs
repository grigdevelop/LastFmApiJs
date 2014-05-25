using System;
using System.Linq;
using LastFmApiJsNet.Api;
using LastFmApiJsNet.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiUnitTest
{
    [TestClass]
    public class TagTest
    {
        [TestMethod]
        public void GetInfo()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var tag = new Tag("rock", session);
            tag = tag.GetInfo();
            tag.appendSession(session);
            Assert.IsNotNull(tag);
        }

        [TestMethod]
        public void GetSimilar()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var tag = new Tag("rock", session);
            var tags = tag.GetSimilar();          
            Assert.IsTrue(tags.Any());
            Assert.IsTrue(!string.IsNullOrWhiteSpace(tags.First().Name));
        }

        [TestMethod]
        public void GetTopAlbums()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var tag = new Tag("rock", session);
            var albums = tag.GetTopAlbums(1, 2);
            Assert.IsTrue(albums.Any());
            Assert.IsTrue(!string.IsNullOrWhiteSpace(albums.First().Name));
        }

        [TestMethod]
        public void GetTopArtists()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var tag = new Tag("rock", session);
            var artists = tag.GetTopArtists(1, 2);
            Assert.IsTrue(artists.Any());
            Assert.IsTrue(!string.IsNullOrWhiteSpace(artists.First().Name));
        }

        [TestMethod]
        public void GetTopTags()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var tag = new Tag("rock", session);
            var tags = tag.GetTopTags();
            Assert.IsTrue(tags.Any());
            Assert.IsTrue(!string.IsNullOrWhiteSpace(tags.First().Name));
        }

        [TestMethod]
        public void GetTopTracks()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var tag = new Tag("rock", session);
            var tracks = tag.GetTopTracks();
            Assert.IsTrue(tracks.Any());
            Assert.IsTrue(!string.IsNullOrWhiteSpace(tracks.First().Name));
        }

        [TestMethod]
        public void GetWeeklyArtistChart()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var tag = new Tag("rock", session);
            var artists = tag.GetWeeklyArtistChart();
            Assert.IsTrue(artists.Any());
            Assert.IsTrue(!string.IsNullOrWhiteSpace(artists.First().Name));
        }

        [TestMethod]
        public void GetWeeklyChartList()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var tag = new Tag("rock", session);
            var charts = tag.GetWeeklyChartList();
            Assert.IsTrue(charts.Any());          
        }

        [TestMethod]
        public void Search()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var tag = new Tag("ro", session);
            var tags = tag.Search();
            Assert.IsTrue(tags.Tags.Any());     
        }
    }
}
