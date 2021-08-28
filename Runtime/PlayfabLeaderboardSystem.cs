using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace OnlineService
{
    public class PlayfabLeaderboardSystem : ALeaderboardSystem
    {

        public override void LoadLeaderBoard(string leaderboardName, int startedPostion, int maxNumberOfRaws)
        {
            var request = new GetLeaderboardRequest
            {
                StatisticName = leaderboardName,
                StartPosition = startedPostion,
                MaxResultsCount = maxNumberOfRaws
            };
            PlayFabClientAPI.GetLeaderboard(request, OnLoadLeaderBoard, Common.OnError);
        }
        void OnLoadLeaderBoard(GetLeaderboardResult result)
        {
            leaderboardData.Clear();
            foreach (var item in result.Leaderboard)
            {
                StructLeaderboardRow record = new StructLeaderboardRow();
                record.postion = item.Position.ToString();
                record.displayName = item.DisplayName.ToString();
                record.stateValue = item.StatValue.ToString();

                Debug.Log(record.postion);
                Debug.Log(record.displayName);
                Debug.Log(record.stateValue);
                leaderboardData.Add(record);
            }
        }

        public override void SaveLeaderBoard(string leaderboardName, int score)
        {

            var request = new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>
                    {
                        new StatisticUpdate
                            {
                            StatisticName=leaderboardName,
                            Value=score
                            }
                    }
            };
            PlayFabClientAPI.UpdatePlayerStatistics(request, OnSaveLeaderBoard, Common.OnError);
        }
        void OnSaveLeaderBoard(UpdatePlayerStatisticsResult result)
        {
            Debug.Log("Score Updated");
        }

        public override List<StructLeaderboardRow> GetLeaderboardData()
        {
            return leaderboardData;
        }
    }
}

