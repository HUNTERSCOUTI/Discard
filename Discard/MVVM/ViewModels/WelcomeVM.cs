namespace Client.MVVM.ViewModels;
#pragma warning disable
public class WelcomeVM
{
    public string CurrentUserName { get; set; }

    public WelcomeVM()
    {
        CurrentUserName = MainVM.CurrentUser.Name;
    }
}