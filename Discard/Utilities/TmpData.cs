using System.Windows.Media.Imaging;
using Client.MVVM.Models;
using Client.MVVM.ViewModels;
using DiscardSERVER.Class_Models;

namespace Client.Utilities;

public static class TmpData
{
    public static void InitializeData()
    {
        MainVM.CurrentUser = new UserModel();
        MainVM.CurrentUser.Name = Environment.UserName;

        string portraitsUrl = "https://randomuser.me/api/portraits/";

        if (new Random().Next(1, 3) == 1)
            portraitsUrl += $"women/{new Random().Next(1, 99)}.jpg";
        else
            portraitsUrl += $"men/{new Random().Next(1, 99)}.jpg";

        MainVM.CurrentUser.ProfilePictureURL = new BitmapImage(new Uri(portraitsUrl));

        // Friends
        for (int i = 0; i < 10; i++)
        {
            FriendModel friend = new FriendModel().NewGeneratedFried();

            MainVM.CurrentUser.FriendList?.Add(friend);
        }

        // Messages
        if (MainVM.CurrentUser.FriendList != null)
            foreach (FriendModel friend in MainVM.CurrentUser.FriendList)
            {
                string[] messages = new string[11];
                for (int j = 0; j < new Random().Next(1, 10); j++)
                {
                    friend.Messages.Add($"Besked {j * new Random().Next()}");
                }
            }
    }
}