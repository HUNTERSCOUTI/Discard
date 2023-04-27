using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable
namespace DiscardSERVER.Class_Models
{
    public class FriendModel
    {
        public string ProfilePictureURL { get; set; }
        public int FriendID { get; set; }
        public string[]? Messages { get; set; }
    }
}
