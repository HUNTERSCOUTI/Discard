using Client.MVVM.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using RandomFriendlyNameGenerator;

#pragma warning disable
namespace DiscardSERVER.Class_Models
{
    public class FriendModel : UserModel
    {
        public ObservableCollection<string> Messages { get; set; }

        public FriendModel(string ProfilePictureURL)
        {
            this.ProfilePictureURL = new BitmapImage(new Uri(ProfilePictureURL));
            this.UserID = new Random().Next();
            this.Messages = new ObservableCollection<string>();
        }

        public FriendModel()
        {
            ProfilePictureURL =
                new BitmapImage(new Uri(
                    "https://www.flaticon.com/free-icon/profile_3135715?term=user&page=1&position=4&origin=search&related_id=3135715"));
            UserID = new Random().Next();
            this.Messages = new ObservableCollection<string>();
        }

        public FriendModel NewGeneratedFried()
        {
            string imageSource = "https://randomuser.me/api/portraits";

            FriendModel friend = new FriendModel();

            friend.UserID = new Random().Next();

            if (new Random().Next(1, 3) == 1)
            {
                friend.ProfilePictureURL =
                    new BitmapImage(new Uri($"{imageSource}/women/{new Random().Next(1, 99)}.jpg"));
                friend.Name = NameGenerator.PersonNames.Get(NameGender.Female);
            }
            else
            {
                friend.ProfilePictureURL =
                    new BitmapImage(new Uri($"{imageSource}/men/{new Random().Next(1, 99)}.jpg"));
                friend.Name = NameGenerator.PersonNames.Get(NameGender.Male);
            }

            return friend;
        }
    }
}