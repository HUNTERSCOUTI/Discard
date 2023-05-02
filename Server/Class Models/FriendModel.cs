using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscardSERVER.Class_Models
{
    public class FriendModel : UserModel
    {
        public string[]? Messages { get; set; }

        public FriendModel(int FriendID, string[]? Messages)
        {
            this.UserID = FriendID;
            this.Messages = Messages;
        }

        public FriendModel()
        {
            this.UserID = new Random().Next();
        }
    }
}
