using System;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace IOTServer
{
    class Server
    {
        private static byte[] result = new byte[1024];
        private static int myPort = 8885;
        static Socket serverSocket;

        public Server()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, myPort));
            serverSocket.Listen(10);

            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();
        }

        private static void ListenClientConnect()
        {
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                clientSocket.Send(Encoding.ASCII.GetBytes("Server Say Hello"));
                Thread receiveThread = new Thread(ReceiveMessage);
                receiveThread.Start(clientSocket);
            }
        }
    }
}
