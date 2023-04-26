using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client.Networking
{
    public class ClientConnection
    {
        TcpClient Connection;
        const int PORT = 31337;

        public void Run()
        {
            Connect(IPAddress.Loopback, PORT);
            while (true)
            {
                string send = Console.ReadLine();
                SendMessage(send);
            }
        }

        public void Connect(IPAddress ip, int port)
        {
            Connection = new TcpClient();
            Connection.Connect(ip, port);

            Thread thread = new Thread(Listener);
            thread.Start();
            Console.WriteLine("Connected To Server");
        }

        /// <summary>
        /// Responsible for always writing others users messages
        /// </summary>
        public void Listener()
        {
            byte[] buffer = new byte[4096];
            NetworkStream stream = Connection.GetStream();

            while (Connection.Connected)
            {
                int read = stream.Read(buffer, 0, buffer.Length);
                string messageFromServer = Encoding.UTF8.GetString(buffer, 0, read);
                Console.WriteLine(messageFromServer);

                buffer = new byte[4096];
            }
        }

        public void SendMessage(string message)
        {
            if (Connection.Connected)
            {
                byte[] bytes = Encoding.UTF8.GetBytes("Zilas: " + message);
                NetworkStream stream = Connection.GetStream();
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        //public void Recieve() { }
    }
}
