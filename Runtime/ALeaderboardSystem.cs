using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OnlineService
{
    public abstract class ALeaderboardSystem
    {
        public List<StructLeaderboardRow> leaderboardData = new List<StructLeaderboardRow>();
        public abstract void SaveLeaderBoard(string leaderboardName, int score);
        public abstract void LoadLeaderBoard(string leaderboardName, int startedPostion, int maxNumberOfRaws);
        public abstract List<StructLeaderboardRow> GetLeaderboardData();
    }
}

