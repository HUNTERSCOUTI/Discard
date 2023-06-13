﻿#nullable disable
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

    public static void ConnectToServer(IPAddress ip, int port, TcpClient Connection, LingerOption _LingerOption)
    {
        _connection = Connection;
        try
        {
            TcpClient connection = new TcpClient();
            connection.LingerState = _LingerOption;
            connection.Connect(ip, port);

            thread = new Thread(Listener);
            thread.Start();

            byte[] bytes = Encoding.UTF8.GetBytes(Environment.UserName);
            NetworkStream stream = connection.GetStream();
            stream.Write(bytes, 0, bytes.Length);
        }
        catch (Exception e)
        {
            MessageBox.Show($"Could not connect to the server, IP ... \n{e.Message}", "Error");
        }
    }

    private static void Listener()
    {
        byte[] buffer = new byte[4096];
        NetworkStream connectionStream = _connection.GetStream();

        try
        {
            while (_connection.Connected)
            {
                int read = connectionStream.Read(buffer, 0, buffer.Length);
                if (read == 0) break;
                string messageFromServer = Encoding.UTF8.GetString(buffer, 0, read);
                //MAKE displayable on WPF HERE

                GlobalChatVM.MessageHistory.Add(messageFromServer);
            }

            connectionStream.Close();
        }
        catch (Exception e)
        {
            MessageBox.Show($"Connection to the server has been lost \n{e.Message}", "Error");
        }
    }

    public static void Disconnect(TcpClient Connection)
    {
        // Disconnect from server
        if (Connection.Connected)
        {
            if (thread.ThreadState == ThreadState.Running)
            {
                
            }
            Connection.Close();
        }
    }
}