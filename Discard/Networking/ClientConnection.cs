#pragma warning disable
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using Client.MVVM.Utilities;
using Client.MVVM.ViewModels;

namespace Client.Networking;

public class ClientConnection : ViewModelBase
{
    private TcpClient Connection;
    private const int PORT = 31337;

    private bool newMessage = false;
    private string _message { get; set; }
    public  string Message  { get; set; }

    public void Run()
    {
        //Connect to own PC
        ConnectToServer(IPAddress.Loopback, PORT);
        //Connect to Zilass
        //ConnectToServer(IPAddress.Parse("192.168.1.153"), PORT);
        while (true)
        {
            if (newMessage == true)
            {
                SendMessage(Message);
                Thread.Sleep(3000);
            }
            else
            {
                Thread.Sleep(3000);
            }
        }
    }

    public void ConnectToServer(IPAddress ip, int port)
    {
        Connection = new TcpClient();

        try
        {
            Connection.Connect(ip, port);
        }
        catch (Exception e)
        {
            MessageBox.Show("Could not connect to server", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        Thread thread = new Thread(Listener);
        thread.Start();
    }

    /// <summary>
    /// Responsible for getting the stream from the server and displaying it.
    /// </summary>
    public void Listener()
    {
        try
        {
            if (Connection.Connected)
            {
                while (Connection.Connected)
                {
                    byte[] buffer = new byte[4096];
                    NetworkStream stream = Connection.GetStream();
                    int read = stream.Read(buffer, 0, buffer.Length);
                    string messageFromServer = Encoding.UTF8.GetString(buffer, 0, read);
                    //MAKE displayable on WPF HERE

                    GlobalChatVM.MessageHistory.Add(messageFromServer);
                }
            }
            else
            {
                MessageBox.Show("Connection to server lost");
                Disconnect();
            }
        }
        catch (Exception e)
        {
            MessageBox.Show("Something went wrong in Listener");
        }
    }

    public void SendMessage(string message)
    {
        try
        {
            if (Connection.Connected)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                NetworkStream stream = Connection.GetStream();
                stream.Write(bytes, 0, bytes.Length);
                newMessage = false;
            }
        }
        catch (Exception e)
        {
            MessageBox.Show("Someting went wrong", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public void Disconnect()
    {
        Connection.Close();
    }
}