using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LastFmApiJsNet.Api;
using LastFmApiJsNet.Services;

namespace LastFmApiTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var session = new Session("405ede2a00cc32568dee9e78300d7df0", "cc124ad78074ec21359b0cc3b94412d1");
            var artist = new Artist("Muse", session);
            var albums = artist.GetTopAlbums();
            foreach (var album in albums) {
                Console.WriteLine("{0} , {1}", album.Name, album.Playcount);
            }

            Console.ReadLine();
        }      
    }
}
