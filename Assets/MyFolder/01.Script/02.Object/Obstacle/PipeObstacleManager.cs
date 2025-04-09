using UnityEngine;

namespace Assets.MyFolder._01.Script._02.Object.Obstacle
{
    public class PipeObstacleManager : MonoBehaviour
    {
        public static PipeObstacleManager Instance { get; private set; }

        [Header("Pipe Settings")]
        [SerializeField] private float pipeMoveSpeed = 5f;

        public float PipeMoveSpeed => pipeMoveSpeed;

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
    }
} 