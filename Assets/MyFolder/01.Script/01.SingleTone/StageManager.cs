using System;
using System.Collections;
using MoreMountains.Feedbacks;
using MyFolder._01.Script._02.Object.Obstacle;
using MyFolder._01.Script._02.Object.Player;
using MyFolder._01.Script._02.Object.Player.State.child;
using MyFolder._01.Script._03.UI;
using MyFolder._01.Script._03.UI.Elemental.TOP;
using MyFolder._01.Script._1001.Admob;
using UnityEngine;

namespace MyFolder._01.Script._01.SingleTone
{
    public class StageManager : DieSingleTone<StageManager>
    {
        [SerializeField] private MMF_Player countdownFeel;
        [SerializeField] private PlayerController player;
        [SerializeField] private LevelManager level;
        [SerializeField] private PipeSpawner pipeSpawn;
        [SerializeField] private BaseUI gameOverUI;
        [SerializeField] private RetryRewardADCount retryRewardADCount;
        [SerializeField] private ScoreManager scoreManager;

        public bool IsGameRunning { get; private set; } = false;
        public Action OnGameStarted;
        public Action OnGameOver;

        public void StartGame()
        {
            OnGameStarted?.Invoke();
            IsGameRunning = true;
        }

        public void StopGame()
        {
            OnGameOver?.Invoke();
            IsGameRunning = false;
            scoreManager.ReportScore();
            UIManager.Instance.OpenUI(gameOverUI);
        }
        void Start()
        {
            countdownFeel.PlayFeedbacks();
            StartCoroutine(WaitForFeedbacks(PlayStart));
        }

        public void ReStart()
        {
            retryRewardADCount.UseRewardedAd();
            countdownFeel.PlayFeedbacks();
            player.ReStartInit();
            pipeSpawn.AllPipeReturn();
            StartCoroutine(WaitForFeedbacks(RePlayStart));
        }
    
        private IEnumerator WaitForFeedbacks(System.Action onComplete)
        {
            yield return new WaitForSeconds(countdownFeel.TotalDuration); // Duration 계산 포함됨
            onComplete?.Invoke();
        }

        private void PlayStart()
        {
            level.PlayStart();
            StartGame();
            player.SetPlayerState<IdleState>();
        }

        private void RePlayStart()
        {
            Debug.Log(Time.timeScale);
            StartGame();
            player.SetPlayerState<IdleState>();
        }
    }
}
