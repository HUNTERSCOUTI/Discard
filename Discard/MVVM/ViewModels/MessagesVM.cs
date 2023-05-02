﻿#pragma warning disable
using System.Windows.Controls;
using System.Windows.Input;
using DiscardSERVER.Class_Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Client.MVVM.Models;
using Client.MVVM.Utilities;
using DiscardSERVER.Class_Models;

namespace Client.MVVM.ViewModels;

public class MessagesVM : ViewModelBase
{
    #region Properties

    private FriendModel _selectedFriend { get; set; }

    public FriendModel SelectedFriend
    {
        get => _selectedFriend;
        set
        {
            _selectedFriend = value;
            OnPropertyChanged();
        }
    }

    private string _userMessage { get; set; }

    public string UserMessage
    {
        get => this._userMessage;
        set
        {
            this._userMessage = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public ICommand SendMessageCommand { get; set; }

    private void SendMessage(object obj)
    {
        if (obj is TextBox textBox)
        {
            SelectedFriend.Messages.Add(textBox.Text);
            textBox.Text = string.Empty;
        }
    }

    #region Constructor

    public MessagesVM(FriendModel selectedFriend)
    {
        SendMessageCommand = new RelayCommand(SendMessage);

        SelectedFriend = selectedFriend;
    }

    #endregion
}