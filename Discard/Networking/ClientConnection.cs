﻿#pragma warning disable
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using Client.MVVM.ViewModels;

namespace Client.Networking
{
    public class ClientConnection
    {
        TcpClient Connection;
        const int PORT = 31337;

        public void Run()
        {
            //Connect to Zilas
            ConnectToServer("127.0.0.1", PORT);
            //ConnectToServer(IPAddress.Parse("172.22.240.1"), PORT);
        }

        public void ConnectToServer(string ip, int port)
        {
            Connection = new TcpClient();
            Connection.Connect(ip, port);

            Thread thread = new Thread(Listener);
            thread.Start();
        }

        /// <summary>
        /// Responsible for getting the stream from the server and displaying it.
        /// </summary>
        public void Listener()
        {
            byte[] buffer = new byte[4096];
            NetworkStream stream = Connection.GetStream();

            try
            {
                while (Connection.Connected)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    string messageFromServer = Encoding.UTF8.GetString(buffer, 0, read);
                    //MAKE displayable on WPF HERE

                    GlobalChatVM.MessageHistory.Add(messageFromServer);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Connection to the server has been lost", "Error");
            }
        }

        public void SendMessage(string message)
        {
            if (Connection.Connected)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                NetworkStream stream = Connection.GetStream();
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        //public void Recieve() { }
    }
}