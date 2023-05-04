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
using RandomFriendlyNameGenerator;

namespace Client.MVVM.ViewModels;

public class MainVM : ViewModelBase
{
    #region Properties

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


    private void GlobalCommand(Object obj) => CurrentView = new GlobalChatVM();

    private void MoveWindow(Object obj)
    {
        if (obj is Window window)
            window.DragMove();
    }

    private void CloseWindow(Object obj)
    {
        Application.Current.Shutdown();

        Client.Disconnect();
    }

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

    private void _tmpData()
    {
        CurrentUser = new UserModel();
        CurrentUser.Name = Environment.UserName;

        string portraitsUrl = "https://randomuser.me/api/portraits/";

        if (new Random().Next(1, 3) == 1)
            portraitsUrl += $"women/{new Random().Next(1, 99)}.jpg";
        else
            portraitsUrl += $"men/{new Random().Next(1, 99)}.jpg";

        CurrentUser.ProfilePictureURL = new BitmapImage(new Uri(portraitsUrl));

        //Friends
        for (int i = 0; i < 20; i++)
        {
            FriendModel friend = new FriendModel().NewGeneratedFried();
            
            CurrentUser.FriendList.Add(friend);
        }

        // Messages
        foreach (FriendModel friend in CurrentUser.FriendList)
        {
            string[] messages = new string[11];
            for (int j = 0; j < new Random().Next(1, 10); j++)
            {
                friend.Messages.Add($"{friend.UserID} : Message Sample {j * new Random().Next()}");
            }
        }
    }

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