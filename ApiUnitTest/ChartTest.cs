using System;
using LastFmApiJsNet.Api;
using LastFmApiJsNet.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiUnitTest
{
    [TestClass]
    public class ChartTest
    {
        [TestMethod]
        public void GetHypedArtists()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var chart = new Chart(session);
            var artists = chart.GetHypedArtists(1, 10);
            Assert.IsTrue(artists.Length == 10);
        }

        [TestMethod]
        public void GetTopArtists()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var chart = new Chart(session);
            var artists = chart.GetTopArtists(1, 10);
            Assert.IsTrue(artists.Length == 10);
        }

        [TestMethod]
        public void GetHypedTracks()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var chart = new Chart(session);
            var tracks = chart.GetHypedTracks(1, 10);
            Assert.IsTrue(tracks.Length == 10);
        }

        [TestMethod]
        public void GetLovedTracks()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var chart = new Chart(session);
            var tracks = chart.GetLovedTracks(1, 10);
            Assert.IsTrue(tracks.Length == 10);
        }

        [TestMethod]
        public void GetTopTracks()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var chart = new Chart(session);
            var tracks = chart.GetTopTracks(1, 10);
            Assert.IsTrue(tracks.Length == 10);
        }

        [TestMethod]
        public void GetTopTags()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var chart = new Chart(session);
            var tags = chart.GetTopTags(1, 10);
            Assert.IsTrue(tags.Length == 10);
        }
    }
}
