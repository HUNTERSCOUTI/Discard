#nullable disable
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using Client.MVVM.ViewModels;

namespace Client.Networking.Utilities;

public static class ServerConnectionUtility
{
    private static TcpClient _connection;
    private static Thread thread;
    private static bool isRunning = true;

    public static void ConnectToServer(TcpClient Connection)
    {
        _connection = Connection;
        try
        {
            thread = new Thread(Listener);
            thread.Start();

            byte[] bytes = Encoding.UTF8.GetBytes(Environment.UserName);
            NetworkStream stream = _connection.GetStream();
            stream.Write(bytes, 0, bytes.Length);
        }
        catch (Exception e)
        {
            MessageBox.Show($"Could not connect to the server, IP ... \n{e.Message}", "Error");
        }
    }

    private static void Listener()
    {

        try
        {
            byte[] buffer = new byte[4096];
            NetworkStream connectionStream = _connection.GetStream();
            
            while (_connection.Connected && isRunning)
            {
                int read = connectionStream.Read(buffer, 0, buffer.Length);
                if (read == 0) break;
                string messageFromServer = Encoding.UTF8.GetString(buffer, 0, read);
                //MAKE displayable on WPF HERE
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
        isRunning = false;
        thread.Join(); // Wait for the thread to finish before continuing
    }
    
    public static void Disconnect(TcpClient Connection)
    {
        // Disconnect from server
        if (Connection.Connected)
        {
            if (thread.ThreadState == ThreadState.Running)
                StopListening();

            Connection.Close();
        }
    }
}