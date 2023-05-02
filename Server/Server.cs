using DiscardSERVER.Class_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Server
    {
        const int PORT = 31337;
        List<UserModel> Users = new();

        public void Start()
        {
            // Open a tcp listener which allows any ip address to connect
            TcpListener listener = new(IPAddress.Any, PORT);
            listener.Start();
            Console.WriteLine($"Server: Lytter på port: {PORT}");
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();

                //Gets the IP of the accepted client
                IPEndPoint remoteIpEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
                Console.WriteLine("IP: {0}", remoteIpEndPoint.Address);

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
                            Console.WriteLine("Message Error");
                        break;
                    }
                }
            });
            //Closes thread when user disconnects
            thread.IsBackground = true;

            thread.Start();
            Console.WriteLine("New User Connected");
        }

        public void DisconnectClient(UserModel user)
        {
            Console.WriteLine($"{user.UserIP} has disconnected from the server");

            user.UserClient.Close();
        }

        public string Receive(UserModel user)
        {
            NetworkStream stream = user.UserClient.GetStream();
            byte[] buffer = new byte[4096];
            int read = stream.Read(buffer, 0, buffer.Length);
            string recieve = Encoding.UTF8.GetString(buffer, 0, read);
            Console.WriteLine($"User Message Recived from {user.UserIP}");

            return recieve;
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
                    //stream.Write(senderNameBytes);
                }
            }
        }
    }
}
