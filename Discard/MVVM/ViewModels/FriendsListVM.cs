using System.Collections.ObjectModel;
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
        
        _friends = new(MainVM.CurrentUser.FriendList);
    }
}