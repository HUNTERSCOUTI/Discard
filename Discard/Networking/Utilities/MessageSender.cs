using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Client.Networking.Utilities;

public static class MessageSender
{
    /// <summary>
    /// Sends a message to the server through the specified TCP connection.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="connection">The TCP connection to use.</param>
    public static void SendMessageToServer(string message, TcpClient? Connection)
    {
        try
        {
            if (Connection != null && Connection.Connected)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                NetworkStream stream = Connection.GetStream();
                stream.Write(bytes, 0, bytes.Length);
            }
        }
        catch (Exception e)
        {
            MessageBox.Show($"Could not send message to the server \n{e.Message}", "Error");
        }
    }
}