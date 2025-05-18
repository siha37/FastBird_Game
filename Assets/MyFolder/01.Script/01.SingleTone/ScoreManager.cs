using GooglePlayGames;
using GooglePlayGames.Android;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace MyFolder._01.Script._01.SingleTone
{
    public class ScoreManager : MonoBehaviour
    {
        public int PNowScore { get; private set; } = 0;
        
        
        [SerializeField] TextMeshProUGUI scoreText;
        
        [SerializeField] MMF_Player scoreFeedback;
        private const string BasicScoreID = "CgklhoaFpdsWEAIQAQ";
        public delegate void SetScoreDelegate(int score);
        public SetScoreDelegate scoreUpPointDelegate;

        public void Awake()
        {
            PNowScore = 0;
            scoreText.text = PNowScore.ToString();
        }

        public void Score_OnPointUp()
        {
            PNowScore++;
            scoreText.text = PNowScore.ToString();
            scoreFeedback.PlayFeedbacks();
            scoreUpPointDelegate?.Invoke(PNowScore);
        }

        public void ReportScore()
        {   
            PlayGamesPlatform.Instance.ReportScore(
                PNowScore,
                BasicScoreID,
                (bool success) =>
                {
                    if (success)
                    {
                        Debug.Log("GPGS 점수 등록 성공!");
                    }
                    else
                    {
                        Debug.LogError("GPGS 점수 등록 실패");
                    }
                }
            );
        }
    }
}
