// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
using System;
using UnityEngine;

namespace OnlineService
{ 
[Serializable]
public class FriendDetails
{
    public int code { get; set; }
    public string status { get; set; }
    public Data data { get; set; }
}

[Serializable]
public struct TitlePlayerAccount
{
        public string Id;
        public string Type;
        public string TypeString;
}

[Serializable]
public struct TitleInfo
{
        public string DisplayName;
        public string Origination;
        public DateTime Created;
        public DateTime LastLogin;
        public DateTime FirstLogin;
        public bool isBanned;
        public TitlePlayerAccount TitlePlayerAccount;
}

[Serializable]
public struct PrivateInfo
{
        public string Email;
}

[Serializable]
public struct UserInfo
{
        public string PlayFabId;
        public DateTime Created;
        public TitleInfo TitleInfo;
        public PrivateInfo PrivateInfo;
}

[Serializable]
public struct Data
{
        public UserInfo UserInfo;
}

}



