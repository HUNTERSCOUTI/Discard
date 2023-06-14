#pragma warning disable
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using Client.Utilities;

namespace Client.MVVM.ViewModels;

public class GlobalChatVM : ViewModelBase
{
    #region Properties

    private static ObservableCollection<String> _messageHistory { get; set; }

    public static ObservableCollection<String> MessageHistory
    {
        get => _messageHistory;
        set
        {
            _messageHistory = value;
            OnPropertyChanged("MessageHistory");
        }
    }

    private string _enteredMessage { get; set; }

    public string EnteredMessage
    {
        get => _enteredMessage;
        set
        {
            this._enteredMessage = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public ICommand SendMessageCommand { get; set; }

    private void SendMessage(object obj)
    {
        if (obj is TextBox textBox)
        {
            SendMessageToServer(textBox.Text.ToString());
            textBox.Text = string.Empty;
        }
    }

    void SendMessageToServer(string message)
    {
        MainVM.Client.SendMessage($"{Environment.UserName}: {message}");
    }

    #region Constructor

    public GlobalChatVM()
    {
        SendMessageCommand = new RelayCommand(SendMessage);
        _messageHistory = new ObservableCollection<string>();
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