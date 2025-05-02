using System;
using MoreMountains.Feedbacks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace MyFolder._01.Script._01.Manager
{
    public class ScoreManager : MonoBehaviour
    {
        public int PNowScore { get; private set; } = 0;
        
        
        [SerializeField] TextMeshProUGUI scoreText;
        
        [SerializeField] MMF_Player scoreFeedback; 
        
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
        
        
    }
}
