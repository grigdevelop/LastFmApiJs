using System;
using System.Linq;
using LastFmApiJsNet.Api;
using LastFmApiJsNet.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiUnitTest
{
    [TestClass]
    public class GeoTest
    {
        [TestMethod]
        public void GetEvents()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var geo = new Geo(session);
            var events = geo.GetEvents(null, null, "list", null, null, null, 1, 10);
            Assert.IsTrue(events.Any());
        }

        [TestMethod]
        public void GetMetroArtistChart()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var geo = new Geo(session);
            var artists = geo.GetMetroArtistChart("madrid", "spain");
            Assert.IsTrue(artists.Any());
        }

        [TestMethod]
        public void GetMetroHypeArtistChart()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var geo = new Geo(session);
            var artists = geo.GetMetroHypeArtistChart("madrid", "spain");
            Assert.IsTrue(artists.Any());
        }

        [TestMethod]
        public void GetMetroHypeTrackChart()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var geo = new Geo(session);
            var tracks = geo.GetMetroHypeTrackChart("madrid", "spain");
            Assert.IsTrue(tracks.Any());
        }

        [TestMethod]
        public void GetMetroTrackChart()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var geo = new Geo(session);
            var tracks = geo.GetMetroTrackChart("madrid", "spain");
            Assert.IsTrue(tracks.Any());
        }

        [TestMethod]
        public void GetMetroUniqueArtistChart()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var geo = new Geo(session);
            var artists = geo.GetMetroUniqueArtistChart("madrid", "spain");
            Assert.IsTrue(artists.Any());
        }

        [TestMethod]
        public void GetMetroUniqueTrackChart()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var geo = new Geo(session);
            var tracks = geo.GetMetroUniqueTrackChart("madrid", "spain");
            Assert.IsTrue(tracks.Any());
        }

        [TestMethod]
        public void GetMetroWeeklyChartlist()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var geo = new Geo(session);
            var charts = geo.GetMetroWeeklyChartlist("madrid");
            Assert.IsTrue(charts.Any());
        }

        [TestMethod]
        public void GetMetros()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var geo = new Geo(session);
            var metros = geo.GetMetros();
            Assert.IsTrue(metros.Any());
        }

        [TestMethod]
        public void GetTopArtists()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var geo = new Geo(session);
            var artists = geo.GetTopArtists("armenia");
            Assert.IsTrue(artists.Any());
        }

        [TestMethod]
        public void GetTopTracks()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var geo = new Geo(session);
            var tracks = geo.GetTopTracks("armenia");
            Assert.IsTrue(tracks.Any());
        }

    }
}
