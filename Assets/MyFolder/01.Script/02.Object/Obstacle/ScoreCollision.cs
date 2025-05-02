using MyFolder._01.Script._02.Object.Object_Pooling;
using UnityEngine;

namespace MyFolder._01.Script._02.Object.Obstacle
{
    public class ScoreCollision : MonoBehaviour
    {
        private Rigidbody2D rb;

        private void Start()
        {
            if (!TryGetComponent(out rb))
            {
                Debug.LogError("Rigidbody2D component not found on PipeObstacle!");
            }
        }

        private void Update()
        {
            // 파이프가 화면 왼쪽 끝을 벗어났는지 확인
            // 파이프의 너비를 고려하여 약간의 여유를 둠
            if (transform.position.x < -40f)
            {
                Vector3 pos = transform.position;
                pos.x = 50;
                transform.position = pos;
                ScorePipeObjectPool.Instance.ReturnPipe(this.gameObject);
            }
        }

        private void FixedUpdate()
        {
            if (rb)
            {
                // 왼쪽으로 이동
                rb.linearVelocity = Vector2.left * PipeObstacleManager.Instance.PipeMoveSpeed;
            }
        }
    }
}