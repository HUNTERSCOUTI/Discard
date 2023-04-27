using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Client.Networking;

namespace Client;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        try
        {
            ClientConnection client = new();

            Thread thread = new(client.Run);
            thread.Start();
        }
        catch (Exception e)
        {
            System.Windows.MessageBox.Show("Could Not Connect to the Server");
        }

        //Fix, does not start WPF program on another thread
        InitializeComponent();
    }

    private void MoveWindow(object sender, MouseButtonEventArgs e)
    {
        this.DragMove();
    }
}