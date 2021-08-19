// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
using System;
using UnityEngine;

namespace OnlineService { 
[Serializable]
public class Root
{
    public int code { get; set; }
    public string status { get; set; }
    public Data data { get; set; }
}

[Serializable]
public class TitlePlayerAccount
{
    public string Id { get; set; }
    public string Type { get; set; }
    public string TypeString { get; set; }
}

[Serializable]
public class TitleInfo
{
    public string DisplayName { get; set; }
    public string Origination { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastLogin { get; set; }
    public DateTime FirstLogin { get; set; }
    public bool isBanned { get; set; }
    public TitlePlayerAccount TitlePlayerAccount { get; set; }
}

[Serializable]
public class PrivateInfo
{
    public string Email { get; set; }
}

[Serializable]
public class UserInfo
{
    public string PlayFabId { get; set; }
    public DateTime Created { get; set; }
    public TitleInfo TitleInfo { get; set; }
    public PrivateInfo PrivateInfo { get; set; }
}

[Serializable]
public class Data
{
    public UserInfo UserInfo { get; set; }
}

}



