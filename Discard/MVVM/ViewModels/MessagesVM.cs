using System.Collections.ObjectModel;
using Client.MVVM.Utilities;
using DiscardSERVER.Class_Models;

namespace Client.MVVM.ViewModels;

public class MessagesVM : ViewModelBase
{
    #region Properties

    private ObservableCollection<string> _messages { get; set; }

    public ObservableCollection<string> Messages
    {
        get => this._messages;
        set
        {
            this._messages = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Constructor

    public MessagesVM(FriendModel friend)
    {
        this._messages = new ObservableCollection<string>();

        foreach (string message in friend.Messages)
        {
            Messages.Add(message);
        }
    }


    public MessagesVM()
    {
    }

    #endregion
}