using System.ComponentModel;

namespace Client.Utilities;

public class StaticOnPropertyChanged
{
    public static event PropertyChangedEventHandler? StaticPropertyChanged;

    // Define static OnPropertyChanged method
    public static void OnPropertyChanged(string name)
    {
        StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
    }

    // Implement INotifyPropertyChanged interface
    public static event PropertyChangedEventHandler? PropertyChanged;

    private static void OnPropertyChangedInstance(string name)
    {
        PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
    }

}