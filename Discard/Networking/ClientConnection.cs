#pragma warning disable
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using Client.MVVM.ViewModels;
using Client.Networking.Utilities;
using static Client.Networking.Utilities.MessageSender;
using static Client.Networking.Utilities.ServerConnectionUtility;

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
            SendMessageToServer(message, Connection);
        }

        public void DisconnectFromServer()
        {
            if (Connection != null && Connection.Connected)
                Connection.Close();
            // Disconnect(Connection);
        }
    }
}