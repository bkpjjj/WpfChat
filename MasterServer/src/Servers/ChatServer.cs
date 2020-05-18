using MasterServer.src.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterServer.src.Servers
{
    class ChatServer
    {
        private Thread m_ServerThread;
        private Socket m_ServerSocket;
        private IPEndPoint m_ServerEndPoint;
        private IPAddress m_ServerIP;
        private int m_ServerPort;
        //
        public IPAddress IPAddress => m_ServerIP;
        public int Port => m_ServerPort;
        public int MaxSize { get; set; }
        public string ServerName { get; set; }

        public ChatServer(string name,IPAddress address,int port)
        {
            ServerName = name;
            //Address
            m_ServerIP = address;
            m_ServerPort = port;
            m_ServerEndPoint = new IPEndPoint(m_ServerIP, m_ServerPort);
            //Socket
            try
            {
                m_ServerSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                m_ServerSocket.Bind(m_ServerEndPoint);
            }
            catch (Exception e)
            {
                Log.Inst.Error($"[{ServerName}] {e.Message}!");
            }
            //
            m_ServerThread = new Thread(new ThreadStart(ServerThread));
        }

        public void Run()
        {
            Log.Inst.Message($"[{ServerName}] Server started!");
            m_ServerThread.Start();
        }

        public void Stop()
        {
            Log.Inst.Message($"[{ServerName}] Server stopped!");
            m_ServerThread.Abort();
            Close();
        }

        private void ServerThread()
        {
            try
            {
                m_ServerSocket.Listen(MaxSize);
                while (true)
                {
                    Log.Inst.Message($"[{ServerName}] Waiting for connection!");
                    Socket connectedSocket = GetConnectedSocket();

                    Log.Inst.Message($"[{ServerName}] User disconected {connectedSocket.RemoteEndPoint}");
                }
            }
            catch (Exception e)
            {
                Log.Inst.Error($"[{ServerName}] {e.Message}!");
            }
        }

        private Socket GetConnectedSocket()
        {
            var socket = m_ServerSocket.Accept();
            Log.Inst.Message($"[{ServerName}] User connected via {socket.RemoteEndPoint}");
            return socket;
        }

        public void Close()
        {
            m_ServerSocket.Close();
        }
    }
}
