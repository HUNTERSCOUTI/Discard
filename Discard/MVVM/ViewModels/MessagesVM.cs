using System.Collections.ObjectModel;
using Client.MVVM.Utilities;
using DiscardSERVER.Class_Models;

#pragma warning disable
namespace Client.MVVM.ViewModels;

public class MessagesVM : ViewModelBase
{
    #region Properties

    private string _friendName { get; set; }

    public string FriendName
    {
        get => this._friendName;
        set
        {
            this._friendName = value;
            OnPropertyChanged();
        }
    }

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
        try
        {
            this.FriendName = friend.FriendID.ToString();
            this._messages = new ObservableCollection<string>();

            foreach (string message in friend.Messages)
            {
                Messages.Add(message);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    #endregion
}