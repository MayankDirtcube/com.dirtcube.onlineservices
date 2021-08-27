using System.Collections.Generic;
namespace OnlineService
{
    public abstract class AFriendSystem
    {
       public abstract void SendFriendRequest(string email);
       public abstract void ReciveFriendRequest();
       public abstract void AcceptFriendRequest(string playerId);
       public abstract void DenyFriendRequest(string playerId);
       public abstract void AddFriend(string PlayerId);
       public abstract void RemoveFriend(string playerId);
       public abstract void GetFriendListfromServer();
       public abstract string[] GetFriendList();
    }
}
