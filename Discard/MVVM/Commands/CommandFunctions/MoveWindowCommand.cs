using System.Windows;

namespace Client.MVVM.Commands.CommandFunctions;

public static class MoveWindowCommand
{
    public static void Execute(Object obj)
    {
        if (obj is Window window)
            window.DragMove();
    }
}