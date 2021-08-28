using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OnlineService { 
public class OnlineServiceManager :Singleton<OnlineServiceManager>
{
    [SerializeField]
    private ServiceType Login;
    [SerializeField]
    private ServiceType Frinds;
    [SerializeField]
    private ServiceType Leaderboard;

    [HideInInspector]
    public ALoginSystem LoginSystem;
    [HideInInspector]
    public AFriendSystem FriendsSystem;
    [HideInInspector]
    public ALeaderboardSystem LeaderboardSystem;


    private void Start()
    {
            switch (Login)
            {
                case ServiceType.Playfab:
                    LoginSystem = new PlayfabLoginSystem();
                    break;
            }

            switch (Frinds)
            {
                case ServiceType.Playfab:
                    FriendsSystem = new PlayfabFriendsSystem();
                   
                    break;
            }

            switch (Leaderboard)
            {
                case ServiceType.Playfab:
                    LeaderboardSystem = new PlayfabLeaderboardSystem();
                    break;
            }
            
    }
}

}