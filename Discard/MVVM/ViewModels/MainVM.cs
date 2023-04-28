using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Client.MVVM.Models;
using Client.MVVM.Utilities;
using DiscardSERVER.Class_Models;

#pragma warning disable
namespace Client.MVVM.ViewModels;

public class MainVM
{
    public static UserModel CurrentUser { get; set; }

    #region ICommands

    public ICommand MoveWindowCommand  { get; set; }
    public ICommand CloseWindowCommand { get; set; }

    #endregion

    #region Commands Function

    private void MoveWindow(Object obj)
    {
        if (obj is Window window)
            window.DragMove();
    }

    private void CloseWindow(Object obj)
    {
        try
        {
            Application.Current.Shutdown();
        }
        catch (Exception e)
        {
            //throws an exception if the application doesnt close
            MessageBox.Show("Could Not Close the Application", "Error");
            throw;
        }
    }

    #endregion

    public MainVM()
    {
        MoveWindowCommand = new RelayCommand(MoveWindow);
        CloseWindowCommand = new RelayCommand(CloseWindow);

        _tmpUser();
        _tmpFriends();
        _tmpMessages();
    }

    private void _tmpUser()
    {
        CurrentUser = new UserModel();

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
}