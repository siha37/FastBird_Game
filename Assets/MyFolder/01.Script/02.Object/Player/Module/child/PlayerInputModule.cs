using Assets.MyFolder._01.Script._02.Object.Player;
using Assets.MyFolder._01.Script._02.Object.Player.Module;
using Assets.MyFolder._01.Script._02.Object.Player.Module.child;
using Assets.MyFolder._01.Script._02.Object.Player.State;
using Assets.MyFolder._01.Script._02.Object.Player.State.child;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyFolder._01.Script._02.Object.Player.Module.child
{
    public class PlayerInputModule : IPlayerTickableModule
    {
        PlayerController player;
        private InputActionAsset actionAsset;
        private InputAction touchAction;
        private InputAction touchInputAction;
        private Vector2 touchStartPosition;
        private float dragThreshold = 50f; // 드래그 감지 임계값
        private bool isDragging = false;

        void IPlayerModule.Init(PlayerController player)
        {
            this.player = player;
            actionAsset = player.inputActionAsset;
            touchAction = actionAsset.FindAction("Touch");
            touchInputAction = actionAsset.FindAction("TouchInput");

            
            // 터치 입력 설정
            touchInputAction.performed += OnTouchInputPerformed;
            touchInputAction.canceled += OnTouchInputCanceled;
        }
        private void OnTouchInputPerformed(InputAction.CallbackContext context)
        {
            Debug.Log("OnTouchPerformed");
            touchStartPosition = touchAction.ReadValue<Vector2>();
            isDragging = false;
        }

        private void OnTouchInputCanceled(InputAction.CallbackContext context)
        {
            Debug.Log("OnTouchCanceled");
            if (!isDragging)
            {
                // 드래그가 아닌 경우 점프 실행
                player.GetModule<PlayerMovement>().Jump();
            }
        }

        void IPlayerTickableModule.Update()
        {
            if (touchInputAction.IsPressed())
            {
                Vector2 currentPosition = touchAction.ReadValue<Vector2>();
                float dragDistance = Vector2.Distance(touchStartPosition, currentPosition);

                if (dragDistance > dragThreshold && !isDragging)
                {
                    isDragging = true;
                    player.SetPlayerState<DashState>();
                }
            }
        }

        void IPlayerTickableModule.FixedUpdate() { }
        void IPlayerTickableModule.LateUpdate() { }
        void IPlayerModule.OnDisable() { }
        void IPlayerModule.OnEnable() { }
        void IPlayerModule.ChangedState(IPlayerState oldstate, IPlayerState newstate) { }

        private void OnDestroy()
        {
            if (touchAction != null)
            {
                touchAction.performed -= OnTouchInputPerformed;
                touchAction.canceled -= OnTouchInputCanceled;
            }
        }
    }
}
