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
                NewClient(new UserModel(client));
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
                        string message = Receive(user.UserClient);
                        Broadcast(message, user);
                    }
                    catch
                    {
                        if(!user.UserClient.Connected)
                            Console.WriteLine("User Disconnected");
                        else
                            Console.WriteLine("Message Error");
                        break;
                    }
                }
            });
            thread.Start();
            Console.WriteLine("New User Connected");
        }

        public string Receive(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[4096];
            int read = stream.Read(buffer, 0, buffer.Length);
            string recieve = Encoding.UTF8.GetString(buffer, 0, read);
            Console.WriteLine("User Message Recived");

            return recieve;
        }

        public void Broadcast(string message, UserModel sender)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] senderNameBytes = Encoding.UTF8.GetBytes(sender.Name);

            foreach (UserModel user in Users)
            {
                if (user.UserClient.Connected && user.UserClient.Equals(sender.UserClient) == false)
                {
                    NetworkStream stream = user.UserClient.GetStream();
                    stream.Write(messageBytes);
                    stream.Write(senderNameBytes);
                }
            }
        }
    }
}
