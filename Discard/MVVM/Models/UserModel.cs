using System.Net.Sockets;
using DiscardSERVER.Class_Models;

#pragma warning disable
namespace Client.MVVM.Models
{
    public class UserModel
    {
        public TcpClient UserClient { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public bool IsOnline { get; set; }
        public List<FriendModel>? FriendList { get; set; }

        public UserModel(TcpClient userClient)
        {
            UserClient = userClient;
        }
    }
}
