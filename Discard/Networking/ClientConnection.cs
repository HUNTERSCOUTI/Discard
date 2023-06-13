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
        public TcpClient? Connection;
        const int PORT = 31337;

        void CreateConnection()
        {
            Connection = new TcpClient();
            Connection.LingerState = new(true, 2);
            Connection.Connect(IPAddress.Loopback, PORT);
        }

        public void Run()
        {
            //Creates Connection then Connects to server
            CreateConnection();
            ConnectToServer(Connection);
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