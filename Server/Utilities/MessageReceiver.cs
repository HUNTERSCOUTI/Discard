using System.Net.Sockets;
using System.Text;
using DiscardSERVER.Class_Models;

namespace Server.Utilities;

public static class MessageReceiver
{
    /// <summary>
    /// Receives a message from the specified user.
    /// </summary>
    /// <param name="user">The UserModel representing the user.</param>
    /// <returns>The received message as a string.</returns>
    public static string Receive(UserModel user)
    {
        while (user.UserClient.Connected)
        {
            NetworkStream stream = user.UserClient.GetStream();
            byte[] buffer = new byte[4096];
            int read = stream.Read(buffer, 0, buffer.Length);
            if (read == 0) break;
            string recieve = Encoding.UTF8.GetString(buffer, 0, read);

            PrintWithColorModel.PrintWithColor($"User Message Received from {user.UserIP}", ConsoleColor.DarkGray);

            return recieve;
        }

        return "Empty Message Received";
    }
}