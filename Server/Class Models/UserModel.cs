using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DiscardSERVER.Class_Models
{
    public class UserModel
    {
        public TcpClient UserClient { get; set; }
        public string? UserIP { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public bool IsOnline { get; set; }
        public List<FriendModel>? FriendList { get; set; }

        public UserModel(TcpClient userClient, string ip)
        {
            UserClient = userClient;
            UserIP = ip;
        }
    }
}
