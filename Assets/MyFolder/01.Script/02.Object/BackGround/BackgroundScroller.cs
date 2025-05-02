using Assets.MyFolder._01.Script._02.Object.Player;
using MyFolder._01.Script._02.Object.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace MyFolder._01.Script._02.Object.BackGround
{
    public class BackgroundScroller : MonoBehaviour
    {
        [SerializeField] private int baseResetPosition = -30; // 기본 리셋 위치
        [SerializeField] private int speedIndex = 0;
        private Rigidbody2D rb;
        private PlayerController playerController;
        private SpriteRenderer spriteRenderer;
        private float SpriteWidth => spriteRenderer.bounds.size.x;
        [SerializeField] private Transform frontBackground;
        private float FrontBackGroundPosX => frontBackground.position.x;
        private float currentmoveDistance;
        private float currentStartPosition;
        private float currentBackgroundWidth;

        public void FrontBackgroundSet(Transform frontBackgroundTf)
        {
            this.frontBackground = frontBackgroundTf;
            currentStartPosition = FrontBackGroundPosX + SpriteWidth;
        }

        public void Start()
        {
            Init();
        }
        public void Init()
        {
            currentmoveDistance = 60 - transform.position.x;
            currentStartPosition = 60;
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            Debug.Log(spriteRenderer.bounds.size.x);
            if (!rb)
            {
                rb = gameObject.AddComponent<Rigidbody2D>();
                rb.gravityScale = 0;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            // PlayerController 찾기
            playerController = FindFirstObjectByType<PlayerController>();
            if (!playerController)
            {
               // Debug.LogError("[BackgroundScroller] PlayerController not found!");
            }
        }
        private void FixedUpdate()
        {
            float speed = BackgroundManager.Instance.BackGroundMoveSpeed(speedIndex) * Time.fixedDeltaTime;
            currentmoveDistance += speed;

            transform.position = new Vector3(currentStartPosition - currentmoveDistance, transform.position.y, transform.position.z);
        }
        private void Update()
        {
            if (rb && playerController)
            {

                // 배경 속도에 따라 리셋 위치와 시작 위치 조정
                //currentBackgroundWidth = baseBackgroundWidth / speedMultiplier;
                //currentResetPosition = -currentBackgroundWidth; // 리셋 위치를 배경 너비에 맞게 조정
                //currentStartPosition = currentBackgroundWidth * 2; // 시작 위치를 배경 너비의 2배로 설정

                // PlayerController의 backgroundSpeed를 곱하여 최종 속도 계산
                /*  
                   rb.linearVelocity = Vector2.left * BackgroundManager.Instance.BackGroundMoveSpeed;

                   // 배경이 화면을 벗어나면 시작 위치로 이동
                   if (transform.position.x <= currentResetPosition)
                   {
                       Vector3 newPosition = transform.position;
                       newPosition.x = currentStartPosition;
                       transform.position = newPosition;
                   }*/
                if(currentStartPosition - currentmoveDistance <= baseResetPosition)
                {
                    currentmoveDistance = 0;
                    currentStartPosition = FrontBackGroundPosX + SpriteWidth;
                    Vector3 newPosition = transform.position;
                    newPosition.x = currentStartPosition;
                    transform.position = newPosition;   
                    //Debug.Log(gameObject.name +"-"+(FrontBackGroundPosX - transform.position.x));
                }
            }
        }
    }
} 