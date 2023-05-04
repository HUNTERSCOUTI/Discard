#pragma warning disable
using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using DiscardSERVER.Class_Models;
using Client.MVVM.Utilities;
using System.ComponentModel;
using System.Threading;
using System.Windows.Media;
using System.Windows.Input;
using Client.MVVM.Models;
using System.Windows;
using Client.Networking;

namespace Client.MVVM.ViewModels;

public class MainVM : ViewModelBase
{
    private static ClientConnection _client { get; set; }
    public static ClientConnection Client
    {
        get => _client;
        set
        {
            _client = value;
            OnPropertyChanged("Client");
        }
    }

   

    private static Object _currentView { get; set; }

    public static Object CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged("CurrentView");
        }
    }

    public static UserModel CurrentUser { get; set; }

    #region ICommands

    public ICommand MoveWindowCommand  { get; set; }
    public ICommand CloseWindowCommand { get; set; }
    public ICommand GlobalChatCommand  { get; set; }

    #endregion

    private void GlobalCommand(Object obj) => CurrentView = new GlobalChatVM();

    private void MoveWindow(Object obj)
    {
        if (obj is Window window)
            window.DragMove();
    }

    private void CloseWindow(Object obj) => Application.Current.Shutdown();

    #endregion

    #region Constructor

    public MainVM()
    {
        try
        {
            _client = new ClientConnection();
            Thread thread = new(_client.Run);
            thread.Start();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        GlobalChatCommand = new RelayCommand(GlobalCommand);
        MoveWindowCommand = new RelayCommand(MoveWindow);
        CloseWindowCommand = new RelayCommand(CloseWindow);

        _currentView = new GlobalChatVM();
    }

    #endregion

    #region tmp

    // private void _tmpData()
    // {
    //     CurrentUser = new UserModel();
    //     CurrentUser.Name = Environment.UserName;
    //
    //     string portraitsUrl = "https://randomuser.me/api/portraits/";
    //
    //     if (new Random().Next(1, 3) == 1)
    //         portraitsUrl += $"women/{new Random().Next(1, 99)}.jpg";
    //     else
    //         portraitsUrl += $"men/{new Random().Next(1, 99)}.jpg";
    //
    //     CurrentUser.ProfilePictureURL = new BitmapImage(new Uri(portraitsUrl));
    //
    //     //Friends
    //     for (int i = 0; i < 20; i++)
    //     {
    //         FriendModel friend = new FriendModel();
    //
    //         friend.UserID = new Random().Next();
    //
    //         https: //randomuser.me/api/portraits/men/12.jpg;
    //         portraitsUrl = "https://randomuser.me/api/portraits/";
    //
    //         if (new Random().Next(1, 3) == 1)
    //             portraitsUrl += $"women/{new Random().Next(1, 99)}.jpg";
    //         else
    //             portraitsUrl += $"men/{new Random().Next(1, 99)}.jpg";
    //
    //         friend.ProfilePictureURL = new BitmapImage(new Uri(portraitsUrl));
    //
    //         CurrentUser.FriendList.Add(friend);
    //     }
    //
    //     //Messages
    //     // foreach (FriendModel friend in CurrentUser.FriendList)
    //     // {
    //     //     string[] messages = new string[11];
    //     //     for (int j = 0; j < 10; j++)
    //     //     {
    //     //         messages[j] = ($"{friend.UserID} : Message Sample {j * new Random().Next()}");
    //     //     }
    //     //
    //     //     friend.Messages = (messages);
    //     // }
    // }

    #endregion

    #region PropertyChanged

    public static event PropertyChangedEventHandler StaticPropertyChanged;

    // Define static OnPropertyChanged method
    private static void OnPropertyChanged(string name)
    {
        StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
    }

    // Implement INotifyPropertyChanged interface
    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChangedInstance(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    #endregion
}