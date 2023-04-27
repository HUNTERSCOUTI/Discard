﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using Client.MVVM.Utilities;
using DiscardSERVER.Class_Models;

#pragma warning disable
namespace Client.MVVM.ViewModels;

public class FriendsListVM : ViewModelBase
{
    #region Properties

    private object _messagesView = new MainVM();

    public object MessagesView
    {
        get => _messagesView;
        set
        {
            _messagesView = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<FriendModel> _friends { get; set; }

    public ObservableCollection<FriendModel> FriendList
    {
        get => _friends;
        set
        {
            _messagesView = value;
            OnPropertyChanged();
        }
    }

    #endregion


    #region ICommands

    public ICommand FriendCommand { get; set; }

    #endregion

    #region

    private void FriendCLicked(Object obj)
    {
        if (obj is FriendModel friend)
            new MessagesVM(friend);

        return;
    }

    #endregion

    public FriendsListVM()
    {
        FriendCommand = new RelayCommand(FriendCLicked);
        _fakeFriends();
    }

    private void _fakeFriends()
    {
        _friends = new ObservableCollection<FriendModel>();

        for (int i = 0; i < 20; i++)
        {
            FriendModel friend = new FriendModel();
            
            Random r = new Random();
            int rInt = r.Next(); //for ints
            friend.FriendID = rInt;

            string[] messages = new string[11];
            for (int j = 0; j < 10; j++)
            {
                messages[j] = ($"Message {j}");
            }


            friend.ProfilePictureURL = "https://generated.photos/face-generator";
            friend.Messages = (messages);
            _friends.Add(friend);
        }
    }
}