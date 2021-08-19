using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

namespace OnlineService
{ 
    public interface ILeaderBoardSystem 
    {
        void SaveLeaderBoard(string leaderboardName,int score);
        void LoadLeaderBoard(string leaderboardName,int startedPostion, int maxNumberOfRaws);
        //List<StructLeaderboardRow> GetLeaderboardData();
    }
}
