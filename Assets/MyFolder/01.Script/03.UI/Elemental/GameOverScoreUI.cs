using System;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using MoreMountains.Feedbacks;
using MyFolder._01.Script._01.SingleTone;
using MyFolder._01.Script._03.UI.Elemental.TOP;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace MyFolder._01.Script._03.UI.Elemental
{
    public class GameOverScoreUI : BaseUI
    {
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private MMF_Player scoreUpFeel;
        [SerializeField] Animator newRecordObject;
        
        TextMeshProUGUI scoreText;
        int score = 0;

        private void OnEnable()
        {
            TryGetComponent(out scoreText);
            ScoreResultOut();
        }

        private void ScoreResultOut()
        {
            newRecordObject.gameObject.SetActive(false);
            score = scoreManager.PNowScore;
/*            ILeaderboard lb = PlayGamesPlatform.Instance.CreateLeaderboard();
            lb.id = "CgklhoaFpdsWEAIQAQ";
            lb.LoadScores(ok =>
            {
                if (ok) {
                    long previous = lb.scores[];
                    if (score > previous)
                    {
                        newRecordObject.gameObject.SetActive(true);
                        newRecordObject.Play("OnEnableAnim");
                        //TODO : 신기록 달성
                    }
                }
                else {
                    Debug.Log("Error retrieving leaderboardi");
                }
            });
*/            
            float scorePerFrame = score / 30f;
            StartCoroutine(nameof(ScoreResultOutCoroutine), scorePerFrame);
        }

        private IEnumerator ScoreResultOutCoroutine(float scorePerFrame)
        {
            int effectScore = 0;
            for (int i = 0; i < 29; i++)
            {
                effectScore += (int)scorePerFrame;
                scoreText.text = effectScore.ToString();
                scoreUpFeel.PlayFeedbacks();
                yield return new WaitForEndOfFrame();
            }
            
            scoreText.text = score.ToString();
            scoreUpFeel.PlayFeedbacks();
        }
    }
}
