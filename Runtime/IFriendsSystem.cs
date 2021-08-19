
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

namespace OnlineService {
    public interface IFriendsSystem
    {
        void SendFriendRequest(string email);
        void ReciveFriendRequest();
        void AcceptFriendRequest(string playfabId);
        void DenyFriendRequest(string playfabId);
        void AddFriend(string playfabId);
        void RemoveFriend(string playfabId);
        void GetFriendListfromServer();
        List<FriendInfo> GetFriendList();

    }
}

