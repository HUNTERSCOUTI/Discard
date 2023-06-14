using DiscardSERVER.Class_Models;
using System.Net;
using System.Net.Sockets;
using Server.Utilities;
using static Server.Utilities.PrintWithColorModel;
using static Server.Utilities.MessageBroadcast;
using static Server.Utilities.MessageReceiver;
using static Server.Utilities.ClientManagement;

namespace Server
{
    public class Server
    {
        private const int Port = 31337;
        List<UserModel> Users = new();

        /// <summary>
        /// Starts the server and listens for incoming connections.
        /// </summary>
        public void Start()
        {
            // Open a TCP listener which allows any IP address to connect
            TcpListener listener = new TcpListener(IPAddress.Loopback, Port);
            listener.Start();

            PrintWithColor($"Server: Listening on port: {Port}", ConsoleColor.Green);

            while (true)
            {
                // Accept a new TCP client connection
                TcpClient client = listener.AcceptTcpClient();

                // Get the IP address of the accepted client
                IPEndPoint? remoteIpEndPoint = client.Client.RemoteEndPoint as IPEndPoint;

                PrintWithColor($"IP: {remoteIpEndPoint!.Address}", ConsoleColor.Blue);

                // Handle the new client connection
                NewClient(new UserModel(client, remoteIpEndPoint.Address.ToString()), Users);
            }
        }
    }
}