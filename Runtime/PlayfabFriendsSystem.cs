using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace OnlineService
{
    public class PlayfabFriendsSystem : IFriendsSystem
    {

        private List<FriendInfo> friendsList = new List<FriendInfo>();
 
        public void AcceptFriendRequest(string playfabId)
        {
            ResponseToFriendRequest("AcceptFriendRequest", playfabId);
        }

        public void DenyFriendRequest(string playfabId)
        {
            ResponseToFriendRequest("DenyFriendRequest", playfabId);
        }

        public void ResponseToFriendRequest(string FunctionNAme, string PlayfabId)
        {
            var request = new ExecuteCloudScriptRequest
            {
                FunctionName = FunctionNAme,
                FunctionParameter = new { FriendPlayFabId = PlayfabId }
            };
            PlayFabClientAPI.ExecuteCloudScript(request, OnResponeToFriednRequest, Common.OnError);
        }
        void OnResponeToFriednRequest(ExecuteCloudScriptResult result)
        {
            Debug.Log(result.FunctionName);
            Debug.Log(result.FunctionResult);
            Debug.Log(result.Logs);
        }

        public void AddFriend(string playfabId)
        {
            var request = new AddFriendRequest
            {
                FriendPlayFabId = playfabId
               
            };
            PlayFabClientAPI.AddFriend(request, OnAddFriend, Common.OnError);
        }
        void OnAddFriend(AddFriendResult result)
        {

        }

        public void GetFriendListfromServer()
        {
            var request = new GetFriendsListRequest
            {
                ProfileConstraints = new PlayerProfileViewConstraints
                {
                    ShowStatistics = true
                }

            };
            
            PlayFabClientAPI.GetFriendsList(request, OnGetFriendsList, Common.OnError);
        }
        void OnGetFriendsList(GetFriendsListResult result)
        {
            friendsList.Clear();
            friendsList = result.Friends;
            Debug.Log("Recive FriendList");
        }

      
        public void SendFriendRequest(string email)
        {
        //    RequestHelper request = new RequestHelper
        //    {
        //        Uri = "https://1b0d0.playfabapi.com/Admin/GetUserAccountInfo",
        //        Method = "POST",
        //        Timeout = 10,
        //        Params = new Dictionary<string, string> {
        //        { "email", email }
        //        //{ "IgnoreMissingTitleActivation", "false" }
        //    },
        //        Headers = new Dictionary<string, string>{
        //        { "X-SecretKey","45BWOADNFP4JS6UB5W1FQ8IR7TRW4BUBSS911FDHYZPNO5QOK5"},
        //        { "Content-Type","application/json" },
        //        { "EntityToken","M3x7ImkiOiIyMDIxLTA3LTA5VDA3OjA3OjE4LjgzMDgyNDJaIiwiaWRwIjoiVW5rbm93biIsImUiOiIyMDIxLTA3LTEwVDA3OjA3OjE4LjgzMDgyNDJaIiwiaCI6IkM1OThBN0EzN0UxRjFDQiIsInMiOiJZRkovbkZLeXo1aHlkTm85QlNpVVMxZkdYZVd1QzladTRIUUptTitodlhjPSIsImVjIjoidGl0bGUhODQyNzQwNDNBRDkzQTlEMC8xQjBEMC8iLCJlaSI6IjFCMEQwIiwiZXQiOiJ0aXRsZSJ9=="}
        //    }
        //    };

        //    RestClient.Request(request).Then(response =>
        //    {
        //        Debug.Log("DEBUGGING RESPONSE" + response.Text);
        //        var FrindObject = JsonUtility.FromJson<Root>(response.Text);
        //        Debug.Log(FrindObject.data.UserInfo.PlayFabId.ToString());
        //    }).Catch(Error =>
        //    {
        //        var errorValue = Error as RequestException;
        //        Debug.Log("Error" + errorValue.Response);
        //    });
        }

        public void ReciveFriendRequest()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveFriend(string playfabId)
        {
            var request = new RemoveFriendRequest
            {
                FriendPlayFabId = playfabId

            };
            PlayFabClientAPI.RemoveFriend(request, OnRemoveFriend, Common.OnError);
        }
        void OnRemoveFriend(RemoveFriendResult result)
        {

        }

        public List<FriendInfo> GetFriendList()
        {
            //GetFriendListfromServer();
            return friendsList;
        }
    }

}

