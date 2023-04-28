using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Client.MVVM.Models;
using Client.MVVM.Utilities;
using DiscardSERVER.Class_Models;

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
        if (obj is Window window)
            window.Close();
    }

    #endregion

    public MainVM()
    {
        MoveWindowCommand = new RelayCommand(MoveWindow);
        CloseWindowCommand = new RelayCommand(CloseWindow);

        _tmpUser();
    }

    private void _tmpUser()
    {
        CurrentUser = new UserModel();
        CurrentUser.ProfilePictureURL = "https://generated.photos/face-generator";
    }

    private void _tmpFriends()
    {
        for (int i = 0; i < 20; i++)
        {
            FriendModel friend = new FriendModel();

            Random r = new Random();
            int rInt = r.Next(); //for ints
            friend.FriendID = rInt;

            CurrentUser.FriendList.Add(friend);
        }
    }

    private void _tmpMessages()
    {
        foreach (FriendModel friend in CurrentUser.FriendList)
        {
            Random r = new Random();
            int rInt = r.Next(); //for ints
            friend.FriendID = rInt;

            string[] messages = new string[11];
            for (int j = 0; j < 10; j++)
            {
                messages[j] = ($"Message {j}");
            }

            CurrentUser.ProfilePictureURL = "https://generated.photos/face-generator";
            friend.Messages = (messages);
        }
    }
}