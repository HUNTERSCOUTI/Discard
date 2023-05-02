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
    }
}