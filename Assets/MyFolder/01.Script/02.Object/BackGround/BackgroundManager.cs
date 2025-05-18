using System.Collections.Generic;
using Assets.MyFolder._01.Script._02.Object.Player;
using MyFolder._01.Script._01.SingleTone;
using MyFolder._01.Script._02.Object.Player;
using UnityEngine;

public class BackgroundManager : SingleTone<BackgroundManager>
{
    [Header("Pipe Settings")]
    [SerializeField] private List<float> backgroundSpeeds = new List<float>(){5};
    private PlayerController playerController;
    public float BackGroundMoveSpeed(int index)
    {
        if (playerController)
        {
            float finalSpeed = backgroundSpeeds[index] * playerController.GetBackgroundSpeed();
#if UNITY_EDITOR
            //Debug.Log($"[BackgroundManager] Current speed: {finalSpeed:F1} (Base: {backgroundSpeeds}, Multiplier: {playerController.GetBackgroundSpeed()})");
#endif
            return finalSpeed;
        }
        return backgroundSpeeds[index];
    }
    private void Start()
    {
        // PlayerController ã��
        playerController = FindFirstObjectByType<PlayerController>();
        if (playerController == null)
        {
            //Debug.LogError("[BackgroundManager] PlayerController not found!");
        }
    }

}
