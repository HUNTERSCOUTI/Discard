using System.Diagnostics;
using Client.MVVM.ViewModels;

namespace Client.MVVM.Commands.CommandFunctions;

public static class CloseWindowCommand
{
    public static void Execute(Object obj)
    {
        MainVM.Client.DisconnectFromServer();
        Process.GetCurrentProcess().Kill();
    }
}