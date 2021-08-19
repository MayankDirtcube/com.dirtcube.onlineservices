using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;
using PlayFab;

namespace OnlineService
{
    public class PSGuiUpdate : MonoBehaviour
    {
        public InputField email;
        public InputField password;
        public InputField Score;
        public InputField FriendEmail;

        public GameObject LeaderBoardPenal;
        public GameObject LeaderboardDataObject;

        public GameObject FriendsListPanal;
        public GameObject FrindsData;
        public Text userNameTxt;
        public Text Currency;

        [SerializeField]
        public string Leaderboard;

        
        private void Start()
        {

            // RE-Re-Testing Submodules
            //PlayFabSettings.TitleId = "24008";
            //Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
            //Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
            
            if (!string.IsNullOrEmpty(PlayerPrefs.GetString("Pushtoken")))
            {
                OnlineServiceManager.Instance.LoginSystem.RegisterForPush();
            }
        }
        

        public void Login()
        {
            OnlineServiceManager.Instance.LoginSystem.LoginByEmail(email.text, password.text);
            userNameTxt.text = OnlineServiceManager.Instance.LoginSystem.GetPlayfabId();
        }

        public void UpdateUI()
        {
            userNameTxt.text = OnlineServiceManager.Instance.LoginSystem.GetPlayfabId();
        }
        public void RegisterByEmail()
        {
            OnlineServiceManager.Instance.LoginSystem.CreateAccountByEmail(email.text, password.text);
        }

        public void SendScoreToLeaderboard()
        {
            OnlineServiceManager.Instance.LeaderboardSystem.SaveLeaderBoard(Leaderboard, int.Parse(Score.text));
        }

        public void GetLeaderboardData()
        {
            OnlineServiceManager.Instance.LeaderboardSystem.LoadLeaderBoard(Leaderboard, 0, 5);

            if (LeaderBoardPenal.transform.childCount > 1)
            {
                for (int i = 1; i < LeaderBoardPenal.transform.childCount; i++)
                {
                    GameObject.Destroy(LeaderBoardPenal.transform.GetChild(i).gameObject);
                }
            }
            //foreach (StructLeaderboardRow item in OnlineServiceManager.Instance.LeaderboardSystem.GetLeaderboardData())
            //{
            //    LeaderBoardPenal.gameObject.SetActive(true);
            //    GameObject data = GameObject.Instantiate(LeaderboardDataObject, LeaderBoardPenal.transform);
            //    data.transform.GetChild(0).GetComponent<Text>().text = item.postion;
            //    data.transform.GetChild(1).GetComponent<Text>().text = item.displayName;
            //    data.transform.GetChild(2).GetComponent<Text>().text = item.stateValue;
            //}
        }

        public void SendFriendRequest()
        {
            OnlineServiceManager.Instance.FriendsSystem.SendFriendRequest(FriendEmail.text);
            
        }

        public void displayFriendsList()
        {
            OnlineServiceManager.Instance.FriendsSystem.GetFriendListfromServer();
            if (FriendsListPanal.transform.childCount > 0)
            {
                for (int i = 0; i < FriendsListPanal.transform.childCount; i++)
                {
                    GameObject.Destroy(FriendsListPanal.transform.GetChild(i).gameObject);
                }
            }
            
          
            foreach (FriendInfo info in OnlineServiceManager.Instance.FriendsSystem.GetFriendList())
            {   
                GameObject frnd;
                string playfabId = "Test"; //info.FriendPlayFabId;
             
                //if (!info.Tags.Contains("confirmed"))
                //{
                    frnd = GameObject.Instantiate(FrindsData,FriendsListPanal.transform);
                    frnd.transform.GetChild(0).GetComponent<Text>().text = info.TitleDisplayName;
                    
                    frnd.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => 
                    {
                            SendCallNotification(info.TitleDisplayName,info.FriendPlayFabId);    
                     });
                    
                    if (info.Tags.Contains("requester"))
                    {
                        frnd.transform.GetChild(3).gameObject.SetActive(false);
                        frnd.transform.GetChild(1).gameObject.SetActive(true);
                        frnd.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { OnlineServiceManager.Instance.FriendsSystem.AcceptFriendRequest(playfabId); });
                        frnd.transform.GetChild(2).gameObject.SetActive(true);
                        frnd.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => { OnlineServiceManager.Instance.FriendsSystem.DenyFriendRequest(playfabId); });
                    }
                //}
            }
        }
        public void AcceptFrindRequest()
        {

        }
        public void DenyFrinedRequest()
        {

        }

        public void Logout()
        {
            OnlineServiceManager.Instance.LoginSystem.Logout();
        }

        void SendCallNotification(string sender, string friendPlayfabId)
        {
            Debug.Log("Frinend ID = "+ friendPlayfabId);
            var request = new ExecuteCloudScriptRequest
            {
                FunctionName = "SendNotificationToFriend",
                FunctionParameter = new {
                    frindPLayfabId = friendPlayfabId,
                    message = sender + " is  caling..."
                }
            };
            PlayFabClientAPI.ExecuteCloudScript(request, OnSendCallNotification,Common.OnError);
            
        }

        void OnSendCallNotification(ExecuteCloudScriptResult result)
        {
            Debug.Log("Notification server function execute pass");
            Debug.Log(result.FunctionName);
        }

        

    }
}

