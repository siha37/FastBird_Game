using Assets.MyFolder._01.Script._02.Object.Player;
using UnityEngine;

public class BackgroundManager : SingleTone<BackgroundManager>
{
    [Header("Pipe Settings")]
    [SerializeField] private float baseBackgroundMoveSpeed = 5f;
    private PlayerController playerController;
    public float BackGroundMoveSpeed
    {
        get
        {
            if (playerController != null)
            {
                float finalSpeed = baseBackgroundMoveSpeed * playerController.GetBackgroundSpeed();
#if UNITY_EDITOR
                Debug.Log($"[BackgroundManager] Current speed: {finalSpeed:F1} (Base: {baseBackgroundMoveSpeed}, Multiplier: {playerController.GetBackgroundSpeed()})");
#endif
                return finalSpeed;
            }
            return baseBackgroundMoveSpeed;
        }
    }
    private void Start()
    {
        // PlayerController Ã£±â
        playerController = FindFirstObjectByType<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("[BackgroundManager] PlayerController not found!");
        }
    }

}
