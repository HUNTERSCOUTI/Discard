using System.Net.Sockets;
using System.Text;
using DiscardSERVER.Class_Models;

namespace Server.Utilities;

public static class MessageBroadcast
{
    /// <summary>
    /// Broadcasts the specified message to all users except the sender.
    /// </summary>
    /// <param name="message">The message to broadcast.</param>
    /// <param name="sender">The UserModel representing the sender.</param>
    /// <param name="users">The list of UserModel representing all users.</param>
    public static void Broadcast(string message, UserModel sender, List<UserModel> users)
    {
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);
        //byte[] senderNameBytes = Encoding.UTF8.GetBytes(sender.Name);

        foreach (UserModel user in users)
        {
            if (user.UserClient.Connected && user.UserClient.Equals(sender.UserClient) == false)
            {
                NetworkStream stream = user.UserClient.GetStream();
                stream.Write(messageBytes);
                PrintWithColorModel.PrintWithColor($"User Message Broadcasted from {user.UserIP}", ConsoleColor.Cyan);
                //stream.Write(senderNameBytes);
            }
        }
    }
}