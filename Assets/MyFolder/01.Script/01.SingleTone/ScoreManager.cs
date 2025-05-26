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
        public int PTotalNowComboScore { get; private set; } = 0;
        public int PNowComboScore { get; private set; } = 0;
        public int PResultScore { get; private set; } = 0;
        
        
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] TextMeshProUGUI comboText;
        
        [SerializeField] MMF_Player scoreFeedback;
        [SerializeField] MMF_Player comboFeedback;
        private const string BasicScoreID = "CgkIhoaFpdsWEAIQAQ";
        public delegate void SetScoreDelegate(int score);
        public SetScoreDelegate scoreUpPointDelegate;
        public SetScoreDelegate comboScoreDelegate;

        public void Awake()
        {
            PNowScore = 0;
            scoreText.text = PNowScore.ToString();
        }

        public void Score_OnPointUp()
        {
            PNowScore++;
            scoreText.text = (PNowScore+PTotalNowComboScore).ToString();
            scoreFeedback.PlayFeedbacks();
            scoreUpPointDelegate?.Invoke(PNowScore);
        }

        public void ComboScore_OnPointUp()
        {
            PNowComboScore += 1;
            comboText.gameObject.SetActive(true);
            comboText.text = "<color=#FFC376>"+PNowComboScore.ToString() + "</color> COMBO";
            comboFeedback.PlayFeedbacks();
            comboScoreDelegate?.Invoke(PNowComboScore);
        }

        public void ComboScore_End()
        {
            if (PNowComboScore == 0)
                return;
            PTotalNowComboScore += PNowComboScore;
            comboText.gameObject.SetActive(false);
            PNowComboScore = 0;
        }

        public void ReportScore()
        {
            PResultScore = PNowScore + PTotalNowComboScore + PNowComboScore;
            PlayGamesPlatform.Instance.ReportScore(
                PResultScore,
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
            PNowComboScore = 0;
            comboText.gameObject.SetActive(false);
        }
    }
}
