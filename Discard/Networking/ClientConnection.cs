#pragma warning disable
using System.Net;
using System.Net.Sockets;
using static Client.Networking.Utilities.MessageSender;
using static Client.Networking.Utilities.ServerConnectionUtility;
using static Client.Networking.Utilities.ConnectionCreater;

namespace Client.Networking
{
    public class ClientConnection
    {
        private const int PORT = 31337;
        private TcpClient? _connection;

        public void Run()
        {
            //Creates Connection then Connects to server
            _connection = CreateTcpConnection(IPAddress.Loopback,PORT);
            ConnectToServer(_connection);
        }

        public void SendMessage(string message)
        {
            SendMessageToServer(message, _connection);
        }

        public void DisconnectFromServer()
        {
            Disconnect(_connection);
        }
    }
}