using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Client.MVVM.Models;
using Client.MVVM.Utilities;
using DiscardSERVER.Class_Models;

#pragma warning disable
namespace Client.MVVM.ViewModels;

public class MainVM : ViewModelBase
{
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

    public static void ChangeView(Object obj)
    {
        // if (obj is MessagesVM() messageView)
        //     CurrentView = new (messageView());

        var a = _currentView;
        var b = CurrentView;
    }

    #region ICommands

    public ICommand MoveWindowCommand  { get; set; }
    public ICommand CloseWindowCommand { get; set; }
    public ICommand HomeCommand { get; set; }

    #endregion

    #region Commands Functions

    private void Home(Object obj) => CurrentView = new WelcomeVM();

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
        HomeCommand = new RelayCommand(Home);
        MoveWindowCommand = new RelayCommand(MoveWindow);
        CloseWindowCommand = new RelayCommand(CloseWindow);

        _tmpUser();
        _tmpFriends();
        _tmpMessages();

        _currentView = new WelcomeVM();
    }

    private void _tmpUser()
    {
        CurrentUser = new UserModel();
        CurrentUser.Name = "John Doe";

        https: //randomuser.me/api/portraits/men/12.jpg;
        string imageUrl = "https://randomuser.me/api/portraits/";

        if (new Random().Next(1, 3) == 1)
            imageUrl += $"women/{new Random().Next(1, 99)}.jpg";
        else
            imageUrl += $"men/{new Random().Next(1, 99)}.jpg";

        CurrentUser.ProfilePictureURL = new BitmapImage(new Uri(imageUrl));
    }

    private void _tmpFriends()
    {
        for (int i = 0; i < 20; i++)
        {
            FriendModel friend = new FriendModel();

            friend.FriendID = new Random().Next();

            https: //randomuser.me/api/portraits/men/12.jpg;
            string imageUrl = "https://randomuser.me/api/portraits/";

            if (new Random().Next(1, 3) == 1)
                imageUrl += $"women/{new Random().Next(1, 99)}.jpg";
            else
                imageUrl += $"men/{new Random().Next(1, 99)}.jpg";

            friend.ProfilePictureURL = new BitmapImage(new Uri(imageUrl));

            CurrentUser.FriendList.Add(friend);
        }
    }

    private void _tmpMessages()
    {
        foreach (FriendModel friend in CurrentUser.FriendList)
        {
            string[] messages = new string[11];
            for (int j = 0; j < 10; j++)
            {
                messages[j] = ($"{friend.FriendID} : Message Sample {j * new Random().Next()}");
            }

            friend.Messages = (messages);
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