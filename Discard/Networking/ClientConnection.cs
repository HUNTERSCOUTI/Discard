#pragma warning disable
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using Client.MVVM.ViewModels;
using static Client.Networking.Models.MessageSender;
using Client.Networking.Models;

namespace Client.Networking
{
    public class ClientConnection
    {
        Thread thread = null;

        private LingerOption _LingerOption = new(true, 2);

        public TcpClient Connection;
        const int PORT = 31337;

        public void Run()
        {
            //Connect to own PC
            ConnectToServer(IPAddress.Loopback, PORT);
        }
        
        public void SendMessage(string message)
        {
            SendMessageToServer(message, Connection);
        }


        public void ConnectToServer(IPAddress ip, int port)
        {
            try
            {
                Connection = new TcpClient();
                Connection.LingerState = _LingerOption;
                Connection.Connect(ip, port);

                thread = new Thread(Listener);
                thread.Start();
                byte[] bytes = Encoding.UTF8.GetBytes(Environment.UserName);
                NetworkStream stream = Connection.GetStream();
                stream.Write(bytes, 0, bytes.Length);
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not connect to the server, IP ...", "Error");
            }
        }

        /// <summary>
        /// Responsible for getting the stream from the server and displaying it.
        /// </summary>
        public void Listener()
        {
            byte[] buffer = new byte[4096];
            NetworkStream connectionStream = Connection.GetStream();

            try
            {
                while (Connection.Connected)
                {
                    int read = connectionStream.Read(buffer, 0, buffer.Length);
                    if (read == 0) break;
                    string messageFromServer = Encoding.UTF8.GetString(buffer, 0, read);
                    //MAKE displayable on WPF HERE

                    GlobalChatVM.MessageHistory.Add(messageFromServer);
                }

                connectionStream.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Connection to the server has been lost", "Error");
            }
        }


        public void DisconnectFromServer()
        {
            // Disconnect from server
            if (Connection.Connected)
            {
                Connection.Close();

                if (thread.ThreadState == ThreadState.Running)
                {
                    // thread.ThreadState = ThreadState.AbortRequested;
                }
            }
        }
    }
}