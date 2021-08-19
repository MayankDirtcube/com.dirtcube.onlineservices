using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Chat;
using PlayFab;
using PlayFab.ClientModels;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
//using Doozy.Engine.UI;

namespace OnlineService
{
    public class PlayfabChat : IChatClientListener
    {
        public string[] ChannelList;
        public int HistoryLengthToFetch;
        string userName;

        public string InputFieldChat;
        public string ChatData;
        public string state;

        string PushNotificationPopupName= "AchievementPopup";
        List<MessageData> ListOfMessagedata = new List<MessageData>();
       // UIPopup m_popup;
        private ChatClient chatClient;

        protected internal ChatAppSettings chatAppSettings;


        public PlayfabChat()
        {
            string[] temp = { "Test" };
            ChannelList = temp;
            HistoryLengthToFetch = 1;
            this.chatClient = new ChatClient(this);
            chatClient.AuthValues = new Photon.Chat.AuthenticationValues();

#if PHOTON_UNITY_NETWORKING
            this.chatAppSettings = GetChatSetting(PhotonNetwork.PhotonServerSettings.AppSettings); 

            #endif
        }

        private ChatAppSettings GetChatSetting(AppSettings appSettings)
        {
            return new ChatAppSettings
            {
                AppIdChat = appSettings.AppIdChat,
                AppVersion = appSettings.AppVersion,
                FixedRegion = appSettings.IsBestRegion ? null : appSettings.FixedRegion,
                NetworkLogging = appSettings.NetworkLogging,
                Protocol = appSettings.Protocol,
                EnableProtocolFallback = appSettings.EnableProtocolFallback,
                Server = appSettings.IsDefaultNameServer ? null : appSettings.Server,
                Port = (ushort)appSettings.Port
            };
        }

        public void Service()
        {
            chatClient?.Service();

        }


        public void GetPhtonToken()
        {
            PlayFabClientAPI.GetPhotonAuthenticationToken(new GetPhotonAuthenticationTokenRequest()
            {
                PhotonApplicationId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime
            },

            (GetPhotonAuthenticationTokenResult result) =>
            {
                chatClient.AuthValues.AddAuthParameter("token", result.PhotonCustomAuthenticationToken);
                Connect();
            },

            Common.OnError); ;
        }

        
        private void Connect()
        {
            
            chatClient.AuthValues.AuthType = Photon.Chat.CustomAuthenticationType.Custom;
            userName =OnlineServiceManager.Instance.LoginSystem.GetPlayfabId();
            chatClient.AuthValues.AddAuthParameter("username",userName);
            chatClient.AuthValues.UserId = userName;
            this.chatClient.ConnectUsingSettings(this.chatAppSettings);
            Debug.Log("Connected to photon chat");

        }

        public void OnClickSend()
        {
            if (this.InputFieldChat != null)
            {
                this.SendChatMessage(InputFieldChat);
                this.InputFieldChat = "";
            }
        }

        [HideInInspector]
        public int TestLength = 2048;
        private byte[] testBytes = new byte[2048];

        private void SendChatMessage(string inputLine)
        {
            if (string.IsNullOrEmpty(inputLine))
            {
                return;
            }

            if ("test".Equals(inputLine))
            {
                if (this.TestLength != this.testBytes.Length)
                {
                    this.testBytes = new byte[this.TestLength];
                }

                this.chatClient.SendPrivateMessage(this.chatClient.AuthValues.UserId, this.testBytes, true);
            }
            else
            {
                chatClient.PublishMessage(ChannelList[0],inputLine);
            }
        }

        public void DebugReturn(DebugLevel level, string message)
        {
            if (level == ExitGames.Client.Photon.DebugLevel.ERROR)
            {
                Debug.LogError(message);
            }
            else if (level == ExitGames.Client.Photon.DebugLevel.WARNING)
            {
                Debug.LogWarning(message);
            }
            else
            {
                Debug.Log(message);
            }
        }

        public void OnChatStateChange(ChatState state)
        {
            this.state = state.ToString();
            Debug.Log(this.state);
        }

        public void OnConnected()
        {

            if (this.ChannelList != null && this.ChannelList.Length > 0)
            {
                this.chatClient.Subscribe(this.ChannelList, this.HistoryLengthToFetch);
                Debug.Log(ChannelList[0]);
            }
            this.chatClient.SetOnlineStatus(ChatUserStatus.Online);
        }

        public void OnDisconnected()
        {
            Debug.Log("Disconnected");
        }

        public void OnGetMessages(string channelName, string[] senders, object[] messages)
        {
                ChatChannel channel = null;
                bool found = this.chatClient.TryGetChannel(channelName, out channel);
                if (!found)
                {
                    Debug.Log("ShowChannel failed to find channel: " + channelName);
                    return;
                }

            
           
            OnScreenPushNotification(senders[senders.Length-1],messages[messages.Length-1].ToString());
            ChatData = channel.ToStringMessages();
         }

        public void OnScreenPushNotification(string sender,string msg)
        {
                #if (UNITY_EDITOR_WIN)

                #else
                            if (sender != userName)
                #endif
            {
                //m_popup = UIPopupManager.GetPopup(PushNotificationPopupName);
                //m_popup.Data.SetLabelsTexts(sender, msg);
                //Debug.Log(sender + " " + msg);
                //UIPopupManager.ShowPopup(m_popup, m_popup.AddToPopupQueue, false);
            }
           
        }

        public void OnPrivateMessage(string sender, object message, string channelName)
        {
            throw new System.NotImplementedException();
        }

        public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
        {
            Debug.Log(user +" now "+ status);
        }

        public void OnSubscribed(string[] channels, bool[] results)
        {
            foreach (string channel in channels)
            {
                this.chatClient.PublishMessage(channel, "says 'hi'.");
            }

            Debug.Log("OnSubscribed: " + string.Join(", ", channels));
        }

        public void OnUnsubscribed(string[] channels)
        {
            
        }

        public void OnUserSubscribed(string channel, string user)
        {
            throw new System.NotImplementedException();
        }

        public void OnUserUnsubscribed(string channel, string user)
        {
            throw new System.NotImplementedException();
        }

        public void OnDestroy()
        {
            if (this.chatClient != null)
            {
                this.chatClient.Disconnect();
            }
        }
        
        public void OnApplicationQuit()
        {
            if (this.chatClient != null)
            {
                this.chatClient.Disconnect();
            }
        }

       
    }
}

