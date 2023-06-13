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
        private LingerOption _LingerOption = new(true, 2);

        public TcpClient? Connection;
        const int PORT = 31337;

        public void Run()
        {
            //Connect to own PC
            ConnectToServer(IPAddress.Loopback, PORT, Connection, _LingerOption);
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