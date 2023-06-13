using DiscardSERVER.Class_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static DiscardSERVER.Class_Models.PrintWithColorModel;

namespace Server
{
    public class Server
    {
        private const int Port = 31337;
        List<UserModel> Users = new();

        public void Start()
        {
            // Open a tcp listener which allows any ip address to connect
            TcpListener listener = new(IPAddress.Loopback, Port);
            listener.Start();
            
            PrintWithColor($"Server: Lytter på port: {Port}", ConsoleColor.Green);
            
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();

                //Gets the IP of the accepted client
                IPEndPoint remoteIpEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
                
                PrintWithColor($"IP: {remoteIpEndPoint.Address}", ConsoleColor.DarkYellow);

                NewClient(new UserModel(client, remoteIpEndPoint.Address.ToString()));
            }
        }

        public void NewClient(UserModel user)
        {
            Users.Add(user);
            Thread thread = new(() =>
            {
                while (true)
                {
                    try
                    {
                        string message = Receive(user);
                        Broadcast(message, user);
                    }
                    catch
                    {
                        if (!user.UserClient.Connected)
                            DisconnectClient(user);
                        else
                            PrintWithColor("Message Error", ConsoleColor.Red);
                        break;
                    }
                }
            });
            //Closes thread when user disconnects
            thread.IsBackground = true;

            thread.Start();
            PrintWithColor("New User Connected", ConsoleColor.DarkGreen);
        }

        public void DisconnectClient(UserModel user)
        {
            PrintWithColor($"{user.UserIP} has disconnected from the server", ConsoleColor.DarkRed);

            user.UserClient.Close();
        }

        public string Receive(UserModel user)
        {
            while (user.UserClient.Connected)
            {
                NetworkStream stream = user.UserClient.GetStream();
                byte[] buffer = new byte[4096];
                int read = stream.Read(buffer, 0, buffer.Length);
                if (read == 0) break;
                string recieve = Encoding.UTF8.GetString(buffer, 0, read);
                
                PrintWithColor($"User Message Received from {user.UserIP}", ConsoleColor.DarkGray);

                return recieve;
            }

            return "Empty Message Received";
        }

        public void Broadcast(string message, UserModel sender)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            //byte[] senderNameBytes = Encoding.UTF8.GetBytes(sender.Name);

            foreach (UserModel user in Users)
            {
                if (user.UserClient.Connected && user.UserClient.Equals(sender.UserClient) == false)
                {
                    NetworkStream stream = user.UserClient.GetStream();
                    stream.Write(messageBytes);
                    PrintWithColor($"User Message Broadcasted from {user.UserIP}", ConsoleColor.Cyan);
                    //stream.Write(senderNameBytes);
                }
            }
        }
    }
}