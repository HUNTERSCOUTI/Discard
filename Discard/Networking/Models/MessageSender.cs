using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Client.Networking.Models;

public static class MessageSender
{
    public static void SendMessageToServer(string message, TcpClient Connection)
    {
        try
        {
            if (Connection.Connected)
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