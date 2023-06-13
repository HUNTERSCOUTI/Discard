namespace Client.Networking.Utilities;

using System;
using System.Net;
using System.Net.Sockets;

public class ConnectionCreater
{
    private const int PORT = 31337;

    public static TcpClient? CreateTcpConnection(IPAddress ip)
    {
        try
        {
            TcpClient? connection = new TcpClient();
            connection.LingerState = new LingerOption(true, 2);
            connection.Connect(ip, PORT);

            return connection;
        }
        catch (SocketException ex)
        {
            Console.WriteLine("Error creating connection: " + ex.Message);
            // Handle the error gracefully
            return null;
        }
    }
}