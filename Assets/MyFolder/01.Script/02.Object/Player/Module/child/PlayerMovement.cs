using Assets.MyFolder._01.Script._02.Object.Player.State;
using Assets.MyFolder._01.Script._02.Object.Player.State.child;
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

        bool IsMove = false;

        public void Init(PlayerController player)
        {
            this.player = player;
            player.TryGetComponent(out rd);
        }

        public void FixedUpdate()
        {
            if(!IsMove) return;
            rd.linearVelocity = new Vector2(player.GetModule<PlayerStatsModule>().CurrentSpeed,rd.linearVelocity.y);
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
            if(newstate is MoveState || newstate is DashState)
            {
                IsMove = true;
            }
            else
            {
                IsMove = false;
            }
        }

    }
}