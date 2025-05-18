using GooglePlayGames;
using UnityEngine;

namespace MyFolder._01.Script._03.UI.ButtonAction.Actions
{
    public class LeaderBoardUIAction : UIButtonAction
    {
        public override void Execute(GameObject sender)
        {  
            PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIhoaFpdsWEAIQAQ");
        }
    }
}