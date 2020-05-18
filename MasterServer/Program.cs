using MasterServer.src.Logger;
using MasterServer.src.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MasterServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ChatServer server = new ChatServer("Server 1",IPAddress.Loopback, 8080);
            server.Run();
        }
    }
}
