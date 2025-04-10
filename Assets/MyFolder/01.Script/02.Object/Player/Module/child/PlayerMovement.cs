using Assets.MyFolder._01.Script._02.Object.Player.State;
using UnityEngine;

namespace Assets.MyFolder._01.Script._02.Object.Player.Module.child
{
    /// <summary>
    /// AnimationController is responsible for controlling the player's animations.
    /// </summary>
    /// <remarks>
    /// This class implements the IPlayerModule interface and provides methods to handle player state changes.
    /// </remarks>
    public class PlayerMovement : IPlayerTickableModule
    {
        PlayerController player;
        Rigidbody2D rd;

        public void Init(PlayerController player)
        {
            this.player = player;
            player.TryGetComponent(out rd);
        }

        public void FixedUpdate()
        {
        }

        public void LateUpdate()
        {
        }

        public void OnDisable()
        {
        }

        public void OnEnable()
        {
        }

        public void Update()
        {
        }

        public void ChangedState(IPlayerState oldstate, IPlayerState newstate)
        {
        }

        public void Jump()
        {
            if (rd)
            {
                float jumpForce = player.GetModule<PlayerStatsModule>().JumpForce;
                // 중력 모듈에 점프 속도 설정
                player.GetModule<PlayerGravityModule>().AddVerticalVelocity(jumpForce);
                // 점프 상태 설정
                player.GetModule<PlayerGravityModule>().SetJumping(true);
            }
        }
    }
}