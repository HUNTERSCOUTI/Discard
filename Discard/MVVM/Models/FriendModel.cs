using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

#pragma warning disable
namespace DiscardSERVER.Class_Models
{
    public class FriendModel
    {
        public ImageSource ProfilePictureURL { get; set; }
        public int         FriendID          { get; set; }
        public string[]?   Messages          { get; set; }

        public FriendModel(string ProfilePictureURL, int FriendID, string[]? Messages)
        {
            this.ProfilePictureURL = new BitmapImage(new Uri(ProfilePictureURL));
            this.FriendID = FriendID;
            this.Messages = Messages;
        }

        public FriendModel()
        {
            this.ProfilePictureURL =
                new BitmapImage(new Uri(
                    "https://www.flaticon.com/free-icon/profile_3135715?term=user&page=1&position=4&origin=search&related_id=3135715"));
            this.FriendID = new Random().Next();
        }
    }
}