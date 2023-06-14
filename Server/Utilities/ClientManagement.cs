namespace Server.Utilities;
using static PrintWithColorModel;
using DiscardSERVER.Class_Models;

public static class ClientManagement
{
    /// <summary>
    /// Handles a new client connection.
    /// </summary>
    /// <param name="user">The UserModel representing the new client.</param>
    /// <param name="users">The list of UserModel representing all connected users.</param>
    public static void NewClient(UserModel user, List<UserModel> users)
    {
        // Add the user to the list of connected users
        users.Add(user);

        // Start a new thread to handle incoming messages from the user
        Thread thread = new Thread(() =>
        {
            while (true)
            {
                try
                {
                    string message = MessageReceiver.Receive(user);
                    MessageBroadcast.Broadcast(message, user, users);
                }
                catch
                {
                    // Disconnect the user if an error occurs during message processing
                    if (!user.UserClient.Connected)
                        DisconnectClient(user);
                    else
                        PrintWithColor("Message Error", ConsoleColor.Red);
                    break;
                }
            }
        });

        // Set the thread as a background thread that will automatically terminate when the application exits
        thread.IsBackground = true;

        // Start the thread to begin processing messages
        thread.Start();

        PrintWithColor("New User Connected", ConsoleColor.DarkGreen);
    }
    
    public static void DisconnectClient(UserModel user)
    {
        PrintWithColor($"{user.UserIP} has disconnected from the server", ConsoleColor.DarkRed);

        user.UserClient.Close();
    }
}