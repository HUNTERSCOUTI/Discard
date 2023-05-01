#pragma warning disable
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Client.Networking
{
    public class ClientConnection
    {
        TcpClient Connection;
        const int PORT = 31337;

        public void Run()
        {
            Connect(IPAddress.Parse("192.168.1.153"), PORT);
            while (true)
            {
                string send = Console.ReadLine();
                SendMessage(send);
            }
        }

        public void Connect(IPAddress ip, int port)
        {
            try
            {
                Connection = new TcpClient();
                Connection.Connect(ip, port);

                Thread thread = new Thread(Listener);
                thread.Start();
                Console.WriteLine("Connected To Server");
            }
            catch (Exception e)
            {
                MessageBox.Show("Could Not Connect to the Server");
            }
        }

        /// <summary>
        /// Responsible for getting the stream from the server and displaying it.
        /// </summary>
        public void Listener()
        {
            byte[] buffer = new byte[4096];
            NetworkStream stream = Connection.GetStream();

            while (Connection.Connected)
            {
                int read = stream.Read(buffer, 0, buffer.Length);
                string messageFromServer = Encoding.UTF8.GetString(buffer, 0, read);
                //MAKE displayable on WPF HERE

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