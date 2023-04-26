using System.Collections.ObjectModel;
using Client.MVVM.Utilities;
using DiscardSERVER.Class_Models;

namespace Client.MVVM.ViewModels;

public class FriendsListVM : ViewModelBase
{
    #region Properties

    private object _currentView = new HomeVM();

    public object CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<FriendModel> _friends { get; set; }

    public ObservableCollection<FriendModel> FriendList
    {
        get => _friends;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    #endregion


    public FriendsListVM()
    {
        _friends = new ObservableCollection<FriendModel>();
        _friends.Add(new FriendModel());
        _friends.Add(new FriendModel());
        _friends.Add(new FriendModel());
        _friends.Add(new FriendModel());
        _friends.Add(new FriendModel());
        
    }
}