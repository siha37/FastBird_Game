using UnityEngine;
using Assets.MyFolder._01.Script._02.Object.Player;

namespace Assets.MyFolder._01.Script._02.Object.Obstacle
{
    public class PipeObstacleManager : MonoBehaviour
    {
        public static PipeObstacleManager Instance { get; private set; }

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
                    Debug.Log($"[PipeObstacleManager] Current speed: {finalSpeed:F1} (Base: {basePipeMoveSpeed}, Multiplier: {playerController.GetBackgroundSpeed()})");
                    return finalSpeed;
                }
                return basePipeMoveSpeed;
            }
        }

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            // PlayerController 찾기
            playerController = FindObjectOfType<PlayerController>();
            if (playerController == null)
            {
                Debug.LogError("[PipeObstacleManager] PlayerController not found!");
            }
        }
    }
} 