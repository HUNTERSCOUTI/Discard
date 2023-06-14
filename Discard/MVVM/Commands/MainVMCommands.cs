using System.Windows.Input;
using Client.Utilities;

namespace Client.MVVM.Commands;

public static class MainVmCommands
{
    public static ICommand? MoveWindowCommand { get; set; }
    public static ICommand? CloseWindowCommand { get; set; }
    public static ICommand? GlobalChatCommand { get; set; }

    public static void InitializeCommands()
    {
        GlobalChatCommand = new RelayCommand(CommandFunctions.GlobalChatCommand.Execute);
        MoveWindowCommand = new RelayCommand(CommandFunctions.MoveWindowCommand.Execute);
        CloseWindowCommand = new RelayCommand(CommandFunctions.CloseWindowCommand.Execute);
    }
}