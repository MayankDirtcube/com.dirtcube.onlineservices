using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Chat;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;

public abstract class AChatSystem : IChatClientListener
{
    private ChatClient chatClient;
    protected internal ChatAppSettings chatAppSettings;

    public AChatSystem()
    {
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

    public abstract void DebugReturn(DebugLevel level, string message);

    public abstract void OnChatStateChange(ChatState state);

    public abstract void OnConnected();

    public abstract void OnDisconnected();

    public abstract void OnGetMessages(string channelName, string[] senders, object[] messages);

    public abstract void OnPrivateMessage(string sender, object message, string channelName);

    public abstract void OnStatusUpdate(string user, int status, bool gotMessage, object message);

    public abstract void OnSubscribed(string[] channels, bool[] results);

    public abstract void OnUnsubscribed(string[] channels);

    public abstract void OnUserSubscribed(string channel, string user);

    public abstract void OnUserUnsubscribed(string channel, string user);
    
}
