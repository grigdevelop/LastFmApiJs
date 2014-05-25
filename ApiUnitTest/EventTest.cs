using System;
using System.Linq;
using LastFmApiJsNet.Api;
using LastFmApiJsNet.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiUnitTest
{
    [TestClass]
    public class EventTest
    {
        [TestMethod]
        public void GetAttendees()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var @event = new Event(3707685, session);
            var users = @event.GetAttendees();
            Assert.IsTrue(users.Any());
        }

        [TestMethod]
        public void GetInfo()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var @event = new Event(3707685, session);
            var info = @event.GetInfo();
            Assert.IsNotNull(info);
            Assert.IsTrue(info.Artists.Any());
            Assert.IsTrue(info.Artists.First().Artists.Any());
        }

        [TestMethod]
        public void GetShouts()
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var @event = new Event(3707685, session);
            var shouts = @event.GetShouts();
            Assert.IsTrue(shouts.Any());
        }

    }
}
