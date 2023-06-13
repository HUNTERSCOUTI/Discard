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
            MessageBox.Show($"⚠️Could not connect to the server!⚠️ \n Check IP And Port Number ...", "Error");
        }
    }

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

    public static void StopListening()
    {
        _isRunning = false;
        _thread.Join(); // Wait for the thread to finish before continuing
    }
    
    public static void Disconnect(TcpClient Connection)
    {
        // Disconnect from server
        if (Connection.Connected)
        {
            if (_thread.ThreadState == ThreadState.Running)
                StopListening();

            Connection.Close();
        }
    }
}