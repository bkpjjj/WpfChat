using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FakeClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var address = IPAddress.Loopback;
            var port = 8080;

            Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(address, port));


            socket.Close();
            Console.ReadLine();
        }
    }
}
