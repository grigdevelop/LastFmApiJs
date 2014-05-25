using System;
using System.Linq;
using LastFmApiJsNet.Api;
using LastFmApiJsNet.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiUnitTest
{
    [TestClass]
    public class LibraryTest
    {
        [TestMethod]
        public void GetAlbums()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var library = new Library("gArLiEgKoSr", session);
            var albums = library.GetAlbums();
            Assert.IsTrue(albums.Any());
        }

        [TestMethod]
        public void GetArtists()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var library = new Library("gArLiEgKoSr", session);
            var artists = library.GetArtists();
            Assert.IsTrue(artists.Any());
        }

        [TestMethod]
        public void GetTracks()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var library = new Library("gArLiEgKoSr", session);
            var tracks = library.GetTracks();
            Assert.IsTrue(tracks.Any());
        }


    }
}
