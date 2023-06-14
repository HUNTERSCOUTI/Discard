namespace Client.Networking.Utilities;

using System;
using System.Net;
using System.Net.Sockets;

public class ConnectionCreater
{
    /// <summary>
    /// Creates a TCP connection with the specified IP address and port number.
    /// </summary>
    /// <param name="ip">The IP address to connect to.</param>
    /// <param name="port">The port number to connect to.</param>
    /// <returns>A TcpClient object representing the connection, or null if an error occurs.</returns>
    public static TcpClient? CreateTcpConnection(IPAddress ip, int port)
    {
        try
        {
            TcpClient? connection = new TcpClient();
            connection.LingerState = new LingerOption(true, 2);
            connection.Connect(ip, port);

            return connection;
        }
        catch (SocketException ex)
        {
            Console.WriteLine($"😒Error creating connection😒: \n{ex.Message}");
            // Handle the error gracefully
            return null;
        }
    }
}