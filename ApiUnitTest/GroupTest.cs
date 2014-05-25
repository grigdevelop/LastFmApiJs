using System;
using System.Linq;
using LastFmApiJsNet.Api;
using LastFmApiJsNet.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiUnitTest
{
    [TestClass]
    public class GroupTest
    {
        [TestMethod]
        public void GetHype()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var group = new Group("radiohead", session);
            var hypes = group.GetHype();
            Assert.IsTrue(hypes.Any());
        }

        [TestMethod]
        public void GetMembers()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var group = new Group("radiohead", session);
            var members = group.GetMembers();
            Assert.IsTrue(members.Any());
        }

        [TestMethod]
        public void GetWeeklyAlbumChart()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var group = new Group("radiohead", session);
            var albums = group.GetWeeklyAlbumChart();
            Assert.IsTrue(albums.Any());
        }

        [TestMethod]
        public void GetWeeklyArtistChart()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var group = new Group("radiohead", session);
            var artists = group.GetWeeklyArtistChart();
            Assert.IsTrue(artists.Any());
        }

        [TestMethod]
        public void GetWeeklyChartList()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var group = new Group("radiohead", session);
            var charts = group.GetWeeklyChartList();
            Assert.IsTrue(charts.Any());
        }

        [TestMethod]
        public void GetWeeklyTrackChart()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var group = new Group("radiohead", session);
            var tracks = group.GetWeeklyTrackChart();
            Assert.IsTrue(tracks.Any());
        }


    }
}
