#nullable disable
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Client.Networking.Utilities;

public static class ServerConnectionUtility
{
    private static TcpClient _connection;
    private static Thread _thread;
    private static bool _isRunning = true;

    /// <summary>
    /// Connects to the server using the specified TCP connection and starts a listener thread.
    /// </summary>
    /// <param name="connection">The TCP connection to use.</param>
    public static void ConnectToServer(TcpClient Connection)
    {
        _connection = Connection;
        try
        {
            _thread = new Thread(Listener);
            _thread.Start();

            byte[] bytes = Encoding.UTF8.GetBytes(Environment.UserName);
            NetworkStream stream = _connection.GetStream();
            stream.Write(bytes, 0, bytes.Length);
        }
        catch (Exception e)
        {
            MessageBox.Show($"⚠️Could not connect to the server!⚠️ \n Check IP And Port Number ... \n{e.Message}", "Error");
        }
    }

    /// <summary>
    /// Listens for incoming messages from the server.
    /// </summary>
    private static void Listener()
    {
        try
        {
            byte[] buffer = new byte[4096];
            NetworkStream connectionStream = _connection.GetStream();

            while (_connection.Connected && _isRunning)
            {
                int read = connectionStream.Read(buffer, 0, buffer.Length);
                if (read == 0) break;
                string messageFromServer = Encoding.UTF8.GetString(buffer, 0, read);
            }

            connectionStream.Close();
        }
        catch (Exception e)
        {
            MessageBox.Show($"Connection to the server has been lost \n{e.Message}", "Error");
        }
    }

    /// <summary>
    /// Stops the listener thread and waits for it to finish.
    /// </summary>
    public static void StopListening()
    {
        _isRunning = false;
        _thread.Join(); // Wait for the thread to finish before continuing
    }

    /// <summary>
    /// Disconnects from the server, stops the listener thread, and closes the connection.
    /// </summary>
    /// <param name="connection">The TCP connection to disconnect from.</param>
    public static void Disconnect(TcpClient Connection)
    {
        //Stops the Running Thread
        if (_thread is not null)
            if (_thread.ThreadState == ThreadState.Running)
                StopListening();

        // Disconnect from server
        if (Connection is not null)
            if (Connection.Connected)
                Connection.Close();
    }
}