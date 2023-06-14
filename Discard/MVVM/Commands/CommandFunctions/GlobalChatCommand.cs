using Client.MVVM.ViewModels;

namespace Client.MVVM.Commands.CommandFunctions;

public static class GlobalChatCommand
{
    public static void Execute(Object obj)
    {
        MainVM.CurrentView = new GlobalChatVM();
    }
}
