using Assets.MyFolder._01.Script._02.Object.Player;
using Assets.MyFolder._01.Script._02.Object.Player.Module;
using Assets.MyFolder._01.Script._02.Object.Player.Module.child;
using MyFolder._01.Script._02.Object.Player.State;
using MyFolder._01.Script._02.Object.Player.State.child;
using UnityEngine.InputSystem;
using UnityEngine;

namespace MyFolder._01.Script._02.Object.Player.Module.child
{
    
    public class PlayerInputModule : IPlayerTickableModule
    {
        public bool TouchAble = true;
        
        PlayerController player;
        private InputAction touchAction;
        private InputAction touchPositionAction;

        private Vector2 touchStartPosition;
        private float dragThreshold = 50f;
        private bool isDragging = false;
        private Vector2 dragStartPosition;
        private bool isTouching = false;
        private float touchStartTime;
        private float touchHoldTime = 0.1f;
        private float positionThreshold = 10f;
        private bool isTouchConfirmed = false;

        void IPlayerModule.Init(PlayerController player)
        {
            this.player = player;

            var actionAsset = player.inputActionAsset; // 예: PlayerInput.inputactions 참조
            var map = actionAsset.FindActionMap("Touch");
            touchAction = map.FindAction("PrimaryContact");
            touchPositionAction = map.FindAction("PrimaryPosition");

            touchAction.Enable();
            touchPositionAction.Enable();
        }

        void IPlayerTickableModule.Update()
        {
            if (!TouchAble) return;
            if (touchAction == null || touchPositionAction == null) return;
            //Debug.Log(touchAction.phase.ToString());
            bool isPressed = touchAction.phase == InputActionPhase.Started || touchAction.phase == InputActionPhase.Performed;


            if (isPressed && !isTouching)
            {
                // Began
                touchStartPosition = touchPositionAction.ReadValue<Vector2>();
                touchStartTime = Time.time;
                isTouching = true;
                isDragging = false;
                isTouchConfirmed = false;
            }
            else if (isPressed && isTouching)
            {
                // Moved / Stationary
                Vector2 currentPosition = touchPositionAction.ReadValue<Vector2>();
                float dragDistance = Vector2.Distance(touchStartPosition, currentPosition);
                float elapsedTime = Time.time - touchStartTime;

                if (!isTouchConfirmed && elapsedTime >= touchHoldTime && !isDragging && dragDistance < positionThreshold)
                {
                    isTouchConfirmed = true;
                    player.GetModule<PlayerMovement>().Jump();
                    return;
                }

                if (dragDistance > dragThreshold && !isDragging)
                {
                    isTouchConfirmed = true;
                    isDragging = true;
                    dragStartPosition = player.transform.position;
                    player.GetModule<PlayerGravityModule>().SetGravityEnabled(false);
                    player.SetPlayerState<DashState>();
                }

                if (isDragging)
                {
                    player.transform.position = new Vector3(
                        player.transform.position.x,
                        dragStartPosition.y,
                        player.transform.position.z
                    );
                }
            }
            else if (!isPressed && isTouching)
            {
                // Ended
                if (isDragging)
                {
                    if (player.GetCurrentState() == nameof(DashState))
                    {
                        player.GetModule<PlayerGravityModule>().SetGravityEnabled(true);
                        player.SetPlayerState<IdleState>();
                    }
                }
                else if (!isTouchConfirmed)
                {
                    player.GetModule<PlayerMovement>().Jump();
                }

                isDragging = false;
                isTouching = false;
                isTouchConfirmed = false;
            }
        }

        void IPlayerTickableModule.FixedUpdate() { }
        void IPlayerTickableModule.LateUpdate() { }
        void IPlayerModule.OnDisable() { }
        void IPlayerModule.OnEnable() { }
        void IPlayerModule.ChangedState(IPlayerState oldstate, IPlayerState newstate) { }
    }
    /*
     * public class PlayerInputModule : IPlayerTickableModule
    {
        PlayerController player;
        private Vector2 touchStartPosition;
        private float dragThreshold = 50f; // 드래그 감지 임계값
        private bool isDragging = false;
        private Vector2 dragStartPosition; // 드래그 시작 시 플레이어 위치
        private bool isTouching = false;
        
        // 터치/드래그 구분을 위한 변수들
        private float touchStartTime;
        private float touchHoldTime = 0.1f; // 터치로 인식하기 위한 최소 시간
        private float positionThreshold = 10f; // 터치로 인식하기 위한 최대 위치 변화량
        private bool isTouchConfirmed = false; // 터치가 확정되었는지 여부

        void IPlayerModule.Init(PlayerController player)
        {
            this.player = player;
        }

        void IPlayerTickableModule.Update()
        {
            // 터치 입력 처리
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        touchStartPosition = touch.position;
                        touchStartTime = Time.time;
                        isTouching = true;
                        isDragging = false;
                        isTouchConfirmed = false;
                        break;

                    case TouchPhase.Stationary:
                    case TouchPhase.Moved:
                        if (isTouching)
                        {
                            Vector2 currentPosition = touch.position;
                            float dragDistance = Vector2.Distance(touchStartPosition, currentPosition);
                            float elapsedTime = Time.time - touchStartTime;

                            // 0.3초가 지났고 위치 변화가 작으면 터치로 인식
                            if (!isTouchConfirmed && elapsedTime >= touchHoldTime && !isDragging && dragDistance < positionThreshold)
                            {
                                isTouchConfirmed = true;
                                Debug.Log("[PlayerInput] Touch confirmed - Jumping");
                                player.GetModule<PlayerMovement>().Jump();
                                isTouching = false;
                                return;
                            }

                            // 드래그 감지
                            if (dragDistance > dragThreshold && !isDragging)
                            {
                                isTouchConfirmed= true; // 드래그가 확정됨
                                isDragging = true;
                                dragStartPosition = player.transform.position;
                                // 중력 멈춤
                                player.GetModule<PlayerGravityModule>().SetGravityEnabled(false);
                                // 배경/파이프 속도 증가
                                player.SetPlayerState<DashState>();
                            }

                            if (isDragging)
                            {
                                // 드래그 중일 때 위치 고정
                                player.transform.position = new Vector3(
                                    player.transform.position.x,
                                    dragStartPosition.y,
                                    player.transform.position.z
                                );
                            }
                        }
                        break;

                    case TouchPhase.Ended:
                        if (isDragging)
                        {
                            if (player.GetCurrentState() == nameof(DashState))
                            {
                                Debug.Log("[PlayerInput] Drag ended - Resetting gravity and speed");
                                // 드래그 종료 시 중력 다시 적용
                                player.GetModule<PlayerGravityModule>().SetGravityEnabled(true);
                                // 배경/파이프 속도 원래대로
                                player.SetPlayerState<IdleState>();   
                            }
                        }
                        else if (isTouching && !isTouchConfirmed)
                        {
                            // 터치가 확정되지 않았고 드래그도 아니었으면 점프
                            player.GetModule<PlayerMovement>().Jump();
                        }
                        isDragging = false;
                        isTouching = false;
                        isTouchConfirmed = false;
                        break;
                }
            }
        }

        void IPlayerTickableModule.FixedUpdate() { }
        void IPlayerTickableModule.LateUpdate() { }
        void IPlayerModule.OnDisable() { }
        void IPlayerModule.OnEnable() { }
        void IPlayerModule.ChangedState(IPlayerState oldstate, IPlayerState newstate) { }
    }
     */
}
