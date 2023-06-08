using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Client.MVVM.Utilities;
using DiscardSERVER.Class_Models;

#pragma warning disable
namespace Client.MVVM.ViewModels;

public class FriendsListVM : ViewModelBase
{
    #region Properties

    private ObservableCollection<FriendModel> _friendsList { get; set; }

    public ObservableCollection<FriendModel> FriendList
    {
        get => _friendsList;
        set
        {
            _friendsList = value;
            OnPropertyChanged();
        }
    }

    #endregion


    #region ICommands

    public ICommand FriendCommand { get; set; }

    #endregion

    #region

    private void FriendClicked(Object obj)
    {
        if (obj is FriendModel friend)
            MainVM.CurrentView = new MessagesVM(friend);
        else
            return;
    }

    #endregion

    public FriendsListVM()
    {
        try
        {
            FriendCommand = new RelayCommand(FriendClicked);
            _friendsList = new(MainVM.CurrentUser.FriendList);
        }
        catch (Exception e)
        {
            ;
        }
    }
}