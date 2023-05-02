using System.Net;
using System.Net.Sockets;
using DiscardSERVER.Class_Models;

namespace DiscardSERVER.Class_Models
{
    public class UserModel
    {
        public TcpClient UserClient { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public bool IsOnline { get; set; }
        public List<FriendModel>? FriendList { get; set; }

        public UserModel(TcpClient userClient, string ip)
        {
            UserClient = userClient;
            this.FriendList = new();
        }

        public UserModel()
        {
            this.UserClient = new();
            this.FriendList = new();
        }
    }
}
