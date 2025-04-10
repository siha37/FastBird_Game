using Assets.MyFolder._01.Script._02.Object.Player.Module;
using Assets.MyFolder._01.Script._02.Object.Player.State;
using UnityEngine;

namespace Assets.MyFolder._01.Script._02.Object.Player.Module.child
{
    public class PlayerGravityModule : IPlayerTickableModule
    {
        [Header("Gravity Settings")]
        [SerializeField] private float baseGravity = 6.9f;
        [SerializeField] private float fallMultiplier = 1.5f;
        [SerializeField] private float lowJumpMultiplier = 1.2f;
        [SerializeField] private float maxFallSpeed = 15f;
        [SerializeField] private float jumpDuration = 0.3f;
        [SerializeField] private float maxRotationAngle = 35f; // 최대 회전 각도
        [SerializeField] private float rotationSpeed = 20f; // 회전 속도

        private PlayerController player;
        private Transform playerTransform;
        private Vector2 currentVelocity;
        private bool isGrounded;
        private bool isJumping;
        private float jumpTimer;
        private float targetRotation; // 목표 회전 각도
        private float currentRotation;
        private bool isGravityEnabled = true; // 중력 활성화 상태

        public void Init(PlayerController player)
        {
            this.player = player;
            playerTransform = player.transform;
            currentVelocity = Vector2.zero;
            targetRotation = 0f;
            Debug.Log("[PlayerGravityModule] Initialized");
        }

        public void OnEnable()
        {
            Debug.Log("[PlayerGravityModule] Enabled");
        }

        public void OnDisable()
        {
            Debug.Log("[PlayerGravityModule] Disabled");
        }

        public void ChangedState(IPlayerState oldstate, IPlayerState newstate)
        {
        }

        public void Update()
        {
            UpdateJumpTimer();
            ApplyGravity();
            UpdatePosition();
            DebugGravityState();
        }

        private void UpdateJumpTimer()
        {
            if (isJumping)
            {
                jumpTimer += Time.deltaTime;
                if (jumpTimer >= jumpDuration)
                {
                    isJumping = false;
                    jumpTimer = 0f;
                    Debug.Log("[PlayerGravityModule] Jump timer reset");
                }
            }
        }

        public void FixedUpdate()
        {
        }

        public void LateUpdate()
        {
        }

        private void ApplyGravity()
        {
            if (!isGravityEnabled)
            {
                // 중력이 비활성화된 경우 회전도 0으로 유지
                targetRotation = 0f;
                return;
            }

            // 점프 중이 아닐 때만 중력 적용
            if (!isJumping)
            {
                // 낙하 중일 때 더 강한 중력 적용
                if (currentVelocity.y < 0)
                {
                    currentVelocity.y += Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
                    targetRotation = -maxRotationAngle; // 아래로 내려갈 때 시계방향 회전
                }
                // 점프 중일 때 더 약한 중력 적용
                else if (currentVelocity.y > 0)
                {
                    currentVelocity.y += Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
                    targetRotation = maxRotationAngle; // 위로 올라갈 때 반시계방향 회전
                }
                else
                {
                    currentVelocity.y += Physics2D.gravity.y * Time.deltaTime;
                    targetRotation = 0f; // 수평 상태
                }

                // 최대 낙하 속도 제한
                if (currentVelocity.y < -maxFallSpeed)
                {
                    currentVelocity.y = -maxFallSpeed;
                }
            }
        }

        private void UpdatePosition()
        {
            Vector3 newPosition = playerTransform.position;
            newPosition.y += currentVelocity.y * Time.deltaTime;
            playerTransform.position = newPosition;

            // 부드러운 회전 적용
            Quaternion currentRotation = playerTransform.rotation;
            Quaternion targetQuaternion = Quaternion.Euler(0, 0, targetRotation);
            playerTransform.rotation = Quaternion.Lerp(currentRotation, targetQuaternion, rotationSpeed * Time.deltaTime);
        }

        private void DebugGravityState()
        {
            //Debug.Log($"[PlayerGravityModule] Current Velocity: {currentVelocity.y}, IsGrounded: {isGrounded}, IsJumping: {isJumping}, JumpTimer: {jumpTimer}");
        }

        public void SetGrounded(bool grounded)
        {
            isGrounded = grounded;
            if (isGrounded)
            {
                currentVelocity.y = 0f;
                targetRotation = 0f; // 지면에 닿으면 수평 상태로 회전
                //Debug.Log("[PlayerGravityModule] Player is grounded");
            }
        }

        public void SetJumping(bool jumping)
        {
            if (jumping && !isJumping)
            {
                isJumping = true;
                jumpTimer = 0f;
                // 점프 시 초기 속도 설정
                currentVelocity.y = player.GetModule<PlayerStatsModule>().JumpForce;
                //Debug.Log($"[PlayerGravityModule] Jump started");
            }
        }

        public void AddVerticalVelocity(float velocity)
        {
            currentVelocity.y = velocity;
            //Debug.Log($"[PlayerGravityModule] Added vertical velocity: {velocity}");
        }

        public float GetCurrentGravity()
        {
            return currentVelocity.y;
        }

        public void SetGravityEnabled(bool enabled)
        {
            isGravityEnabled = enabled;
            if (!enabled)
            {
                currentVelocity.y = 0f; // 중력 비활성화 시 수직 속도 초기화
                targetRotation = 0f; // 회전도 0으로 초기화
                playerTransform.rotation = Quaternion.identity; // 현재 회전도 0으로 설정
                Debug.Log($"[PlayerGravityModule] Gravity disabled - Reset rotation to 0°");
            }
            else
            {
                Debug.Log($"[PlayerGravityModule] Gravity enabled");
            }
        }
    }
} 