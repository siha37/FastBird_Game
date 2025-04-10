using UnityEngine;
using Assets.MyFolder._01.Script._02.Object.Player;

namespace Assets.MyFolder._01.Script._02.Object.Background
{
    public class BackgroundScroller : MonoBehaviour
    {
        [SerializeField] private float baseScrollSpeed = 5;
        [SerializeField] private int baseBackgroundWidth = 30; // 기본 배경 너비
        [SerializeField] private int baseResetPosition = -30; // 기본 리셋 위치
        [SerializeField] private int baseStartPosition = 60; // 기본 시작 위치
        
        private Rigidbody2D rb;
        private PlayerController playerController;
        private float currentResetPosition;
        private float currentStartPosition;
        private float currentBackgroundWidth;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                rb = gameObject.AddComponent<Rigidbody2D>();
                rb.gravityScale = 0;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            // PlayerController 찾기
            playerController = FindFirstObjectByType<PlayerController>();
            if (!playerController)
            {
                Debug.LogError("[BackgroundScroller] PlayerController not found!");
            }

            // 초기 위치 설정
            currentResetPosition = baseResetPosition;
            currentStartPosition = baseStartPosition;
            currentBackgroundWidth = baseBackgroundWidth;
        }

        private void FixedUpdate()
        {
            if (rb && playerController)
            {
                float speedMultiplier = playerController.GetBackgroundSpeed();
                
                // 배경 속도에 따라 리셋 위치와 시작 위치 조정
                //currentBackgroundWidth = baseBackgroundWidth / speedMultiplier;
                //currentResetPosition = -currentBackgroundWidth; // 리셋 위치를 배경 너비에 맞게 조정
                //currentStartPosition = currentBackgroundWidth * 2; // 시작 위치를 배경 너비의 2배로 설정

                // PlayerController의 backgroundSpeed를 곱하여 최종 속도 계산
                float finalSpeed = baseScrollSpeed * speedMultiplier;
                rb.linearVelocity = Vector2.left * finalSpeed;

                // 배경이 화면을 벗어나면 시작 위치로 이동
                if (transform.position.x <= currentResetPosition)
                {
                    Vector3 newPosition = transform.position;
                    newPosition.x = currentStartPosition;
                    transform.position = newPosition;
                }
            }
        }
    }
} 