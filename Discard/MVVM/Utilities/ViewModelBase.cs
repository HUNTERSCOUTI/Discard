using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
#pragma warning disable
namespace Client.MVVM.Utilities;

public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string? propName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}