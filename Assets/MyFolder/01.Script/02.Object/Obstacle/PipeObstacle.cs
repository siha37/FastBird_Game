using UnityEngine;
using Assets.MyFolder._01.Script._02.Object.Object_Pooling;

namespace Assets.MyFolder._01.Script._02.Object.Obstacle
{
    public class PipeObstacle : MonoBehaviour
    {
        private Camera mainCamera;
        private Rigidbody2D rb;

        private void Start()
        {
            mainCamera = Camera.main;
            if (!TryGetComponent(out rb))
            {
                Debug.LogError("Rigidbody2D component not found on PipeObstacle!");
            }
        }

        private void Update()
        {
            if (!mainCamera) return;

            // 월드 좌표를 뷰포트 좌표로 변환 (0~1 사이의 값)
            Vector3 viewportPoint = mainCamera.WorldToViewportPoint(transform.position);
            
            // 파이프가 화면 왼쪽 끝을 벗어났는지 확인
            // 파이프의 너비를 고려하여 약간의 여유를 둠
            if (viewportPoint.x < -0.1f)
            {
                PipeObjectPool.Instance.ReturnPipe(this.gameObject);
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