using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using MyFolder._01.Script._02.Object.Player;
using MyFolder._01.Script._02.Object.Player.State.child;
using UnityEngine;

public class StageManager : DieSingleTone<StageManager>
{
    [SerializeField] private MMF_Player countdownFeel;
    [SerializeField] private PlayerController player;
    [SerializeField] private LevelManager level;

    void Start()
    {
        countdownFeel.PlayFeedbacks();
        StartCoroutine(WaitForFeedbacks(PlayStart));
    }

    private IEnumerator WaitForFeedbacks(System.Action onComplete)
    {
        yield return new WaitForSeconds(countdownFeel.TotalDuration); // Duration 계산 포함됨
        onComplete?.Invoke();
    }

    private void PlayStart()
    {
        level.PlayStart();
        player.SetPlayerState<IdleState>();
    }
}
