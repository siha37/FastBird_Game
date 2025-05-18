using Assets.MyFolder._01.Script._02.Object.Player;
using MyFolder._01.Script._01.SingleTone;
using MyFolder._01.Script._02.Object.Player;
using UnityEngine;

namespace MyFolder._01.Script._02.Object.Obstacle
{
    public class PipeObstacleManager : SingleTone<PipeObstacleManager>
    {

        [Header("Pipe Settings")]
        [SerializeField] private float basePipeMoveSpeed = 5f;

        private PlayerController playerController;

        public float PipeMoveSpeed
        {
            get
            {
                if (playerController)
                {
                    float finalSpeed = basePipeMoveSpeed * playerController.GetBackgroundSpeed();
#if UNITY_EDITOR
                    //Debug.Log($"[PipeObstacleManager] Current speed: {finalSpeed:F1} (Base: {basePipeMoveSpeed}, Multiplier: {playerController.GetBackgroundSpeed()})");
#endif
                    return finalSpeed;
                }
                return basePipeMoveSpeed;
            }
        }

        private void Start()
        {
            // PlayerController 찾기
            playerController = FindFirstObjectByType<PlayerController>();
            if (playerController == null)
            {
                Debug.LogError("[PipeObstacleManager] PlayerController not found!");
            }
        }
    }
} 