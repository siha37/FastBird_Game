using UnityEngine;
using Assets.MyFolder._01.Script._02.Object.Player;

namespace Assets.MyFolder._01.Script._02.Object.Obstacle
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
                if (playerController != null)
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